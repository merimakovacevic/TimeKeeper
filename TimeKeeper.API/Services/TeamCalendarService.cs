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
    public class TeamCalendarService
    {
        protected UnitOfWork unit;
        public TeamCalendarService(UnitOfWork _unit)
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
    }
}
