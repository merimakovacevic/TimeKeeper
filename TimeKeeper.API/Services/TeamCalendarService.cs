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
        public List<TeamTimeTrackingModel> TeamMonthReport(int teamId, int month, int year)
        {
            List<TeamTimeTrackingModel> teamTimeTracking = new List<TeamTimeTrackingModel>();
            List<Day> days = unit.Calendar.Get(x => x.Date.Year == year && x.Date.Month == month).ToList();
            List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            Team team = unit.Teams.Get(x => x.Id == teamId).FirstOrDefault();
            foreach (Member member in team.Members)
            {
                teamTimeTracking.Add(new TeamTimeTrackingModel { Employee = member.Employee });
                List<Day> employeeDays = days.FindAll(x => x.Employee.Id == member.Employee.Id);
                int missingEntries = employeeDays.Count * 8;
                foreach (DayType dayType in dayTypes)
                {
                    List<Day> dayTypeDays = employeeDays.FindAll(x => x.DayType.Id == dayType.Id);
                    int sum = (int)dayTypeDays.Sum(x => x.TotalHours);
                    missingEntries -= sum;
                    teamTimeTracking[teamTimeTracking.Count() - 1].hourTypes.Add(dayType.Name, sum);
                }
                teamTimeTracking[teamTimeTracking.Count() - 1].hourTypes.Add("Missing entries", missingEntries);
            }
            return teamTimeTracking;
        }

        public TeamTimeTrackingModel PersonalReportMonth(Employee employee, int month, int year, List<Project> employeeProjects = null)
        {
            

            TeamTimeTrackingModel employeePersonalReport = new TeamTimeTrackingModel
            {
                Employee = employee,
                hourTypes=new Dictionary<string, decimal>()
            };

            List<Day> employeeDays = employee.Calendar.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();

            SetDayTypes(employeePersonalReport.hourTypes);

            decimal missingEntries = employee.Calendar.Count * 8;
            decimal totalHours = 0;

            foreach(Day day in employeeDays)
            {
                employeePersonalReport.hourTypes[day.DayType.Name] += day.TotalHours;
                totalHours += day.TotalHours;
                missingEntries -= day.TotalHours;
            }

            employeePersonalReport.hourTypes.Add("Missing entries", missingEntries);
            employeePersonalReport.hourTypes.Add("Total hours", totalHours);
            return employeePersonalReport;
        }

        private /*Dictionary<string, int>*/ void SetDayTypes(Dictionary<string, decimal> hourTypes)
        {
            List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            foreach(DayType day in dayTypes)
            {
                hourTypes.Add(day.Name, 0);
            }
            //return hourTypes;
        }

        public TeamTimeTrackingModel nekaMetoda(int month, int year, int employeeId)
        {
            TeamTimeTrackingModel teamTimeTracking = new TeamTimeTrackingModel();

            List<DayType> dayTypes = unit.DayTypes.Get().ToList();
            teamTimeTracking.Employee = unit.Employees.Get(employeeId);

            List<Day> employeeDays = unit.Employees.Get(employeeId).Calendar.Where(x => x.Date.Year == year && x.Date.Month == month).ToList();

            int missingEntries = employeeDays.Count * 8;
                            foreach (DayType dayType in dayTypes)
                            {
                                List<Day> dayTypeDays = employeeDays.FindAll(x => x.DayType.Id == dayType.Id);
                int sum = (int)dayTypeDays.Sum(x => x.TotalHours);
                missingEntries -= sum;
                                teamTimeTracking.hourTypes.Add(dayType.Name, sum);
                            }
            teamTimeTracking.hourTypes.Add("Missing entries", missingEntries);

            return teamTimeTracking;
        }
    }
}
