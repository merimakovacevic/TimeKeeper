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
        public CalendarService(UnitOfWork _unit)
        {
            unit = _unit;
        }
        public List<EmployeeTimeModel> TeamMonthReport(int teamId, int month, int year)
        {
            List<EmployeeTimeModel> teamTimeTracking = new List<EmployeeTimeModel>();
            List<Day> days = unit.Calendar.Get(x => x.Date.Year == year && x.Date.Month == month).ToList();
            List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            Team team = unit.Teams.Get(x => x.Id == teamId).FirstOrDefault();
            foreach (Member member in team.Members)
            {
                teamTimeTracking.Add(new EmployeeTimeModel { Employee = member.Employee.Create() });
                List<Day> employeeDays = days.FindAll(x => x.Employee.Id == member.Employee.Id);
                int missingEntries = employeeDays.Count * 8;
                foreach (DayType dayType in dayTypes)
                {
                    List<Day> dayTypeDays = employeeDays.FindAll(x => x.DayType.Id == dayType.Id);
                    int sum = (int)dayTypeDays.Sum(x => x.TotalHours);
                    missingEntries -= sum;
                    teamTimeTracking[teamTimeTracking.Count() - 1].HourTypes.Add(dayType.Name, sum);
                }
                teamTimeTracking[teamTimeTracking.Count() - 1].HourTypes.Add("Missing entries", missingEntries);
            }
            return teamTimeTracking;
        }

        //This method creates an EmployeeTimeModel for a month, an given employee, and the wanted projects
        public EmployeeTimeModel CreateEmployeeReport(Employee employee, int year, int month, List<Project> projects = null)
        {
            EmployeeTimeModel employeePersonalReport = employee.CreateTimeModel();

            List<Day> employeeDays = employee.Calendar.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();
            SetDayTypes(employeePersonalReport.HourTypes);

            Dictionary<string, decimal> hours = employeePersonalReport.HourTypes;

            if(projects == null)
            {
                projects = Services.GetEmployeeProjects(unit, employee.Id);
            }

            hours.Add("Missing entries", employeeDays.Count * 8);
            hours.Add("Total hours", 0);

            foreach (Project project in projects)
            {
                foreach (Day day in employeeDays)
                {
                    //Adds the day's total hours to the appropriate place in the dictionary (to the appropriate day type)
                    //hours[day.DayType.Name] += day.JobDetails.Where(x => x.Project.Id == project.Id).Sum(jd => jd.Hours); 
                    hours[day.DayType.Name] += CalculateHoursOnProject(day, project);
                    hours["Total hours"] += day.TotalHours;
                    hours["Missing entries"] -= day.TotalHours;
                }
            }            

            return employeePersonalReport;
        }

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

        //private EmployeeTimeModel CreateEmployeePersonalReport(List<Day> employeeDays)

        private void SetDayTypes(Dictionary<string, decimal> hourTypes)
        {
            List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            foreach (DayType day in dayTypes)
            {
                hourTypes.Add(day.Name, 0);
            }
        }





        public void PersonalReport( Member member, int month, int year)
        {
            /*TeamTimeTrackingModel personalReport = new TeamTimeTrackingModel
            {
                Employee = member.Employee,

            }*/
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
                if (day < emp.BeginDate || (emp.EndDate!=new DateTime(1,1,1) && emp.EndDate != null && day > emp.EndDate)) newDay.DayType = na.Master();
                calendar.Add(newDay);
                day = day.AddDays(1);
            }


            List<DayModel> employeeDays = unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).ToList();
            foreach(var d in employeeDays)
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

            if(date > DateTime.Today.AddMonths(6))
            {
                return false;
            }
            return true;
        }
    }
}
