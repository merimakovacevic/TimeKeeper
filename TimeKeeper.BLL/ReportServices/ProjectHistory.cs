using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using TimeKeeper.BLL.Utilities;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.DTO.ReportModels.ProjectHistory;

namespace TimeKeeper.BLL.ReportServices
{
    public class ProjectHistory : BLLBaseService
    {
        public ProjectHistory(UnitOfWork unit) : base(unit)
        {

        }
        public List<MonthProjectHistoryModel> GetMonthlyProjectHistory(int projectId, int employeeId)
        {
            //We use this object to get month names
            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();

            List<JobDetail> employeeTasks = _unit.Projects.Get(projectId).Tasks.ToList()
                                                         .Where(x => x.Day.Employee.Id == employeeId).ToList();

            List<MonthProjectHistoryModel> monthProjectHistory = new List<MonthProjectHistoryModel>();

            //The same SetYearsColumns is used for model initialisation in GetMonthlyProjectHistory and GetProjectHistory
            for (int i = 1; i <= 12; i++)
            {
                monthProjectHistory.Add(new MonthProjectHistoryModel
                {
                    MonthNumber = i,
                    MonthName = dateInfo.GetMonthName(i),
                    HoursPerYears = _unit.Projects.Get(projectId).SetYearsColumns(),
                    TotalHoursPerMonth = 0
                });
            }

            foreach (JobDetail task in employeeTasks)
            {
                /*We use the months index number in monthProjectHistory, 
                 * which will be task.Day.Date.Month - 1, 
                 * so for instance, january will have the index of 0, february the index of 1*/
                monthProjectHistory[task.Day.Date.Month - 1].HoursPerYears[task.Day.Date.Year] += task.Hours;
                monthProjectHistory[task.Day.Date.Month - 1].TotalHoursPerMonth += task.Hours;
            }

            return monthProjectHistory;
        }
        public ProjectHistoryModel GetProjectHistoryModel(int projectId)
        {
            ProjectHistoryModel projectHistory = new ProjectHistoryModel();

            List<JobDetail> tasks = _unit.Projects.Get(projectId).Tasks.ToList();
            List<Employee> employees = new List<Employee>();

            foreach (JobDetail a in tasks)
            {
                if (_unit.Employees.Get().ToList().FirstOrDefault(x => x.Id == a.Day.Employee.Id) != null && !employees.IsDuplicateEmployee(a.Day.Employee))
                {
                    employees.Add(_unit.Employees.Get(a.Day.Employee.Id));
                }
            }

            foreach (Employee emp in employees)
            {
                EmployeeProjectHistoryModel e = new EmployeeProjectHistoryModel
                {
                    EmployeeName = emp.FullName,
                    HoursPerYears = _unit.Projects.Get(projectId).SetYearsColumns(),
                    TotalHoursPerProjectPerEmployee = 0
                };
                foreach (JobDetail a in tasks)
                {
                    if (a.Day.Employee.Id == emp.Id && e.HoursPerYears.ContainsKey(a.Day.Date.Year))
                    {
                        e.HoursPerYears[a.Day.Date.Year] += a.Hours;
                    }
                }
                foreach (KeyValuePair<int, decimal> keyValuePair in e.HoursPerYears)
                {
                    e.TotalHoursPerProjectPerEmployee += keyValuePair.Value;
                }
                projectHistory.Employees.Add(e);
            }
            projectHistory.TotalYearlyProjectHours = _unit.Projects.Get(projectId).SetYearsColumns();

            foreach (EmployeeProjectHistoryModel empProjectModel in projectHistory.Employees)
            {
                foreach (KeyValuePair<int, decimal> keyValuePair in empProjectModel.HoursPerYears)
                {
                    projectHistory.TotalYearlyProjectHours[keyValuePair.Key] += keyValuePair.Value;
                }
                projectHistory.TotalHoursPerProject += empProjectModel.TotalHoursPerProjectPerEmployee;
            }
            return projectHistory;
        }

        public ProjectHistoryAnnualModel GetStored(int projectId)
        {
            ProjectHistoryAnnualModel result = new ProjectHistoryAnnualModel();
            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from ProjectHistory({projectId})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            
            List<HistoryRawData> rawData = new List<HistoryRawData>();
            if (sql.HasRows)
            {
                while (sql.Read())
                {
                    rawData.Add(new HistoryRawData
                    {
                        EmployeeId = sql.GetInt32(0),
                        EmployeeName = sql.GetString(1),
                        Hours = sql.GetDecimal(2),
                        Year = sql.GetInt32(3)
                    });
                }
                HashSet<int> set = new HashSet<int>();

                result.Years = rawData.Select(x => x.Year).Distinct().ToList();

                EmployeeProjectHistory total = new EmployeeProjectHistory(result.Years) { Employee = new MasterModel { Id = 0, Name = "TOTAL" } };
                EmployeeProjectHistory eph = new EmployeeProjectHistory(result.Years) { Employee = new MasterModel { Id = 0 } };
                
                foreach (HistoryRawData item in rawData)
                {
                    if (item.EmployeeId != eph.Employee.Id)
                    {
                        if (eph.Employee.Id != 0) result.Employees.Add(eph);
                        eph = new EmployeeProjectHistory(result.Years)
                        {
                            Employee = new MasterModel { Id = item.EmployeeId, Name = item.EmployeeName }
                        };
                    }
                    eph.TotalYearlyProjectHours[item.Year] = item.Hours;
                    eph.TotalHoursPerProject += item.Hours;
                    total.TotalYearlyProjectHours[item.Year] += item.Hours;
                    total.TotalHoursPerProject += item.Hours;
                }
                if (eph.Employee.Id != 0) result.Employees.Add(eph);
                result.Employees.Add(total);
            }
            
            return result;
        }
    }
}
