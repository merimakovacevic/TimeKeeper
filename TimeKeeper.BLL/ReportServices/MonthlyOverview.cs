using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels.MonthlyOverview;
using TimeKeeper.DTO.ReportModels.ProjectHistory;

namespace TimeKeeper.BLL.ReportServices
{
    public class MonthlyOverview : BLLBaseService
    {
        public MonthlyOverview(UnitOfWork unit) : base(unit) { }

        //THE COLUMN HEADERS AREN'T ORDERED AS THE CORRESPONDING HOURS THAT REPRESENT EACH COLUMN IN INDIVIDUAL ROWS!!!
        public MonthlyTimeModel GetMonthlyOverview(int year, int month)
        {
            MonthlyTimeModel pmm = new MonthlyTimeModel();

            var source = _unit.Tasks.Get(d => d.Day.Date.Year == year && d.Day.Date.Month == month).ToList();
            var tasks = source.OrderBy(x => x.Day.Employee.Id)
                              .ThenBy(x => x.Project.Id)
                              .ToList();

            pmm.Projects = tasks.GroupBy(p => new { p.Project.Id, p.Project.Name })
                                .Select(p => new MasterModel { Id = p.Key.Id, Name = p.Key.Name })
                                .ToList();
            List<int> pList = pmm.Projects.Select(p => p.Id).ToList();

            var details = tasks.GroupBy(x => new {
                empId = x.Day.Employee.Id,
                empName = x.Day.Employee.FullName,
                projId = x.Project.Id,
                projName = x.Project.Name
            })
                                            .Select(y => new
                                            {
                                                employee = new MasterModel { Id = y.Key.empId, Name = y.Key.empName },
                                                project = new MasterModel { Id = y.Key.projId, Name = y.Key.projName },
                                                hours = y.Sum(h => h.Hours)
                                            }).ToList();

            EmployeeProjectModel epm = new EmployeeProjectModel(pList) { Employee = new MasterModel { Id = 0 } };
            foreach (var item in details)
            {
                if (epm.Employee.Id != item.employee.Id)
                {
                    if (epm.Employee.Id != 0) pmm.Employees.Add(epm);
                    epm = new EmployeeProjectModel(pList) { Employee = item.employee };
                }
                epm.Hours[item.project.Id] += item.hours;
                epm.TotalHours += item.hours;
            }
            if (epm.Employee.Id != 0) pmm.Employees.Add(epm);

            return pmm;
        }

        public MonthlyTimeModel GetStored(int year, int month)
        {
            MonthlyTimeModel result = new MonthlyTimeModel();

            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from MonthlyReport({year},{month})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            List<MonthlyRawData> rawData = new List<MonthlyRawData>();
            if (sql.HasRows)
            {
                while (sql.Read())
                {
                    rawData.Add(new MonthlyRawData
                    {
                        EmpId = sql.GetInt32(0),
                        EmpName = sql.GetString(1),
                        ProjId = sql.GetInt32(2),
                        ProjName = sql.GetString(3),
                        Hours = sql.GetDecimal(4)
                    });
                }

                result.Projects = rawData.GroupBy(x => new { x.ProjId, x.ProjName })
                                         .Select(x => new MasterModel { Id = x.Key.ProjId, Name = x.Key.ProjName }).ToList();

                List<int> projList = result.Projects.Select(x => x.Id).ToList();
                EmployeeProjectModel total = new EmployeeProjectModel(projList) { Employee = new MasterModel { Id = 0, Name="TOTAL" } };
                total.TotalHours = 0;
                EmployeeProjectModel epm = new EmployeeProjectModel(projList) { Employee = new MasterModel { Id = 0 } };
                foreach (MonthlyRawData item in rawData)
                {
                    //is it new employee?
                    if (item.EmpId != epm.Employee.Id)
                    {
                        if (epm.Employee.Id != 0) result.Employees.Add(epm);
                        epm = new EmployeeProjectModel(projList)
                        {
                            Employee = new MasterModel { Id = item.EmpId, Name = item.EmpName }
                        };
                    }
                    epm.Hours[item.ProjId] = item.Hours;
                    epm.TotalHours += item.Hours;
                    total.Hours[item.ProjId] += item.Hours;
                    total.TotalHours += item.Hours;
                }
                if (epm.Employee.Id != 0) result.Employees.Add(epm);
                result.Employees.Add(total);
            }

            return result;
        }
    }
}
