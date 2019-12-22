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
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.BLL.ReportServices
{
    public class TimeTracking: CalendarService
    {
        protected List<string> _dayTypes;
        public TimeTracking(UnitOfWork unit): base(unit)
        {
            _dayTypes = unit.DayTypes.Get().Select(x => x.Name).ToList();
        }
        /*
        public List<EmployeeTimeModel> GetStored(int teamId, int year, int month)
        {
            List<EmployeeTimeModel> result = new List<EmployeeTimeModel>();

            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from TeamEmployees({teamId},{year},{month})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            List<TeamTimeRawModel> rawData = new List<TeamTimeRawModel>();
            List<EmployeeTimeModel> employeeTimeModels = new List<EmployeeTimeModel>();
            List<int> empList = new List<int>();
            if (sql.HasRows)
            {
                while (sql.Read())
                {
                    empList.Add(sql.GetInt32(0));
                }
                foreach(int item in empList)
                {
                    Employee emp = _unit.Employees.Get(item);
                    EmployeeTimeModel employeeTime = GetEmployeeMonthReport(emp, year, month);
                   // employeeTime.Role = member.Role.Name;
                    employeeTimeModels.Add(employeeTime);
                }

            }
            return employeeTimeModels;
        }
        */
        public List<EmployeeTimeModel> GetTeamMonthReport(int teamId, int year, int month)
        {
            return GetTeamMonthReport(_unit.Teams.Get(teamId), year, month);
        }
        public List<EmployeeTimeModel> GetTeamMonthReport(Team team, int year, int month)
        {
            //Show only team members that were active during this month
            //filter total hours > 0?
            //Team team = _unit.Teams.Get(teamId);
            List<EmployeeTimeModel> employeeTimeModels = new List<EmployeeTimeModel>();

            foreach (Member member in team.Members)
            {
                employeeTimeModels.Add(GetEmployeeMonthReport(member.Employee, year, month));
            }
            return employeeTimeModels;
        }

        public EmployeeTimeModel GetEmployeeMonthReport(Employee employee, int year, int month)
        {
            //TOTAL HOURS IN DASHBOARD - MONTHLY THEORETICAL WORKING HOURS
            EmployeeTimeModel employeeReport = new EmployeeTimeModel(_dayTypes);            
            employeeReport.Employee = employee.Master();
          
            List<DayModel> calendar = GetEmployeeMonth(employee, year, month);

            //this is to shorten down the Dictionary name
            Dictionary<string, decimal> hours = employeeReport.HourTypes;

            foreach (DayModel day in calendar)
            {
                if (day.DayType.Name == "Empty") hours["Missing entries"] += 8;

                if (_dayTypes.FirstOrDefault(x => x == day.DayType.Name) != null)
                {

                    /*The Get method in the generic Repository throws an exception if the entity isn't found, 
                     * so it is necessary to try to get the daytype from the database.*/
                    hours[day.DayType.Name] += day.TotalHours;
                    employeeReport.TotalHours += day.TotalHours;

                    //Is it better for this to be in a separate method, considering the application performance?
                    if (day.IsWorkday()) employeeReport.AddOvertime(day);

                    /*if the total recorded hours for a Workday are less than 8, the difference is added to the missing entries*/
                    /*If tasks are added to weekend day, the day is saved as a workday. In that case, it is not necessary to add
                     the difference to the missing entries*/
                    if (day.TotalHours < 8 && !day.IsWeekend()) //is this necessary? SHOULD WE DELETE IT?
                    {
                        hours["Missing entries"] += 8 - day.TotalHours;
                    }
                    //Any additional day types to be added as paid time off? Other (Id = 7)?
                    if (!day.IsWorkday())
                    {
                        employeeReport.PaidTimeOff += day.TotalHours;
                    }
                }
            }

            //hours["Missing entries"] = calendar.FindAll(x => x.DayType.Name == "Empty").Count() * 8;
            //Missing entries are included in the Total hours sum
            employeeReport.TotalHours += hours["Missing entries"];

            return employeeReport;
        }

        public EmployeeMissingEntries GetEmployeeMissingEntries(Employee employee, int year, int month)
        {
            EmployeeMissingEntries employeeMissing = new EmployeeMissingEntries();
            employeeMissing.Employee = employee.Master();
            employeeMissing.MissingEntries = 0;

            List<DayModel> calendar = GetEmployeeMonth(employee, year, month);

            foreach (DayModel day in calendar)
            {
                if (day.DayType.Name == "Empty") employeeMissing.MissingEntries += 8;
            }
            return employeeMissing;
        }

        public TeamMissingEntries GetTeamMissingEntries(Team team, int year, int month)
        {
            TeamMissingEntries teamMissing = new TeamMissingEntries();
            teamMissing.Team = team.Master();
            teamMissing.Hours = 0;

            foreach(Member member in team.Members)
            {
                EmployeeMissingEntries employeeMissing = GetEmployeeMissingEntries(member.Employee, year, month);
                teamMissing.Hours += employeeMissing.MissingEntries;
            }
            return teamMissing;
        }
    }
}
