using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Services
{
    public class CalendarService
    {
        protected UnitOfWork unit;
        protected List<DayType> dayTypesInMemory;
        public CalendarService(UnitOfWork _unit)
        {
            unit = _unit;
            dayTypesInMemory = unit.DayTypes.Get().ToList();
            dayTypesInMemory.Add(new DayType { Id = 10, Name = "Future" });
            dayTypesInMemory.Add(new DayType { Id = 11, Name = "Empty" });
            dayTypesInMemory.Add(new DayType { Id = 12, Name = "Weekend" });
            dayTypesInMemory.Add(new DayType { Id = 13, Name = "N/A" });
        }

        public TeamDashboardModel GetTeamDashboard(int teamId, int year, int month)
        {

            TeamDashboardModel teamDashboard = new TeamDashboardModel();

            teamDashboard.EmployeeTimes = GetTeamMonthReport(teamId, year, month);
            teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
            teamDashboard.ProjectsCount = unit.Teams.Get(teamId).Projects.Count();

            foreach(EmployeeTimeModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.HourTypes["Total hours"];
                teamDashboard.WorkingHours += employeeTime.HourTypes["Workday"];
            }

            return teamDashboard;
        }

        public List<EmployeeTimeModel> GetTeamMonthReport(int teamId, int year, int month)
        {
            Team team = unit.Teams.Get(teamId);
            List<EmployeeTimeModel> employeeTimeModels = new List<EmployeeTimeModel>();

            foreach (Member member in team.Members)
            {
                employeeTimeModels.Add(GetEmployeeMonthReport(member.Employee.Id, year, month));
            }

            return employeeTimeModels;
        }

        public EmployeeTimeModel GetEmployeeMonthReport(int employeeId, int year, int month)
        {
            Employee employee = unit.Employees.Get(employeeId);
            EmployeeTimeModel employeePersonalReport = employee.CreateTimeModel();
            List<DayModel> calendar = GetEmployeeMonth(employeeId, year, month);

            SetDayTypes(employeePersonalReport.HourTypes);

            Dictionary<string, decimal> hours = employeePersonalReport.HourTypes;

            hours.Add("Total hours", 0);

            foreach (DayModel day in calendar)
            {
                hours[day.DayType.Name] += day.TotalHours;//CalculateHoursOnProject(day, project);
                hours["Total hours"] += day.TotalHours;
                if (day.DayType.Name == "Workday" && day.TotalHours > 8)
                {
                    employeePersonalReport.Overtime += day.TotalHours - 8;
                }   
                //Any additional day types to be added as paid time off?
                if(day.DayType.Name != "Workday")
                {
                    employeePersonalReport.PaidTimeOff += day.TotalHours;
                }
            }
            hours.Add("Missing entries", calendar.FindAll(x => x.DayType.Name == "Empty").Count() * 8);

            return employeePersonalReport;
        }
 
        public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!ValidateMonth(year, month)) throw new Exception("Invalid data! Check year and month");
            DayType future = new DayType { Id = 10, Name = "Future" };
            DayType empty = new DayType { Id = 11, Name = "Empty" };
            DayType weekend = new DayType { Id = 12, Name = "Weekend" };
            DayType na = new DayType { Id = 13, Name = "N/A" };
            DateTime day = new DateTime(year, month, 1);
            Employee emp = unit.Employees.Get(empId);
            while (day.Month == month)
            {
                DayModel newDay = new DayModel
                {
                    Employee = emp.Master(),
                    Date = day,
                    DayType = empty.Master()
                };
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday) newDay.DayType = weekend.Master();
                if (day > DateTime.Today) newDay.DayType = future.Master();
                if (day < emp.BeginDate || (emp.EndDate != new DateTime(1, 1, 1) && emp.EndDate != null && day > emp.EndDate)) newDay.DayType = na.Master();
                calendar.Add(newDay);
                day = day.AddDays(1);
            }
            List<DayModel> employeeDays = unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).ToList();

            foreach (var d in employeeDays)
            {
                calendar[d.Date.Day - 1] = d;
            }
            return calendar;
        }

        public bool ValidateMonth(int year, int month)
        {
            if (month > 12 || month < 1) return false;
            //founding year
            if (year < 2000) return false;
            DateTime date = new DateTime(year, month, 1);
            if (date > DateTime.Today.AddMonths(6))
            {
                return false;
            }
            return true;
        }

        private void SetDayTypes(Dictionary<string, decimal> hourTypes)
        {
            //List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            foreach (DayType day in dayTypesInMemory)
            {
                hourTypes.Add(day.Name, 0);
            }
        }

                /*
         private decimal CalculateHoursOnProject(Day employeeDay, Project project)
         {
             decimal totalHoursOnProject = 0;
             foreach (JobDetail jobDetail in employeeDay.JobDetails)
             {
                 if (project.Id == jobDetail.Project.Id)
                 {
                     totalHoursOnProject += jobDetail.Hours;
                 }
             }

             return totalHoursOnProject;
         }

         private decimal GetMonthlyWorkingHours(int year, int month)
         {
             int daysInMonth = DateTime.DaysInMonth(year, month);
             int workingDays = 0;

             for (int i = 1; i <= daysInMonth; i++)
             {
                 DateTime thisDay = new DateTime(year,month,i);
                 if(thisDay.DayOfWeek != DayOfWeek.Saturday && thisDay.DayOfWeek != DayOfWeek.Sunday)
                 {
                     workingDays += 1;
                 }
             }

             return workingDays;
         }*/


    }
}
