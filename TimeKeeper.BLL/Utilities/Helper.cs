using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels;

namespace TimeKeeper.BLL.Utilities
{
    public static class Helper
    {
        public static Dictionary<string, decimal> SetMonthlyOverviewColumns(this List<Project> projects)
        {
            Dictionary<string, decimal> projectColumns = new Dictionary<string, decimal>();
            foreach (Project project in projects)
            {
                projectColumns.Add(project.Name, 0);
            }
            return projectColumns;
        }
        //PROJECT HISTORY
        public static Dictionary<int, decimal> SetYearsColumns(this Project project)
        {
            Dictionary<int, decimal> yearColumns = new Dictionary<int, decimal>();
            List<JobDetail> tasks = project.Tasks.ToList();
            foreach (JobDetail a in tasks)
            {
                if (!yearColumns.IsDuplicateYear(a.Day.Date.Year))
                    yearColumns.Add(a.Day.Date.Year, 0);
            }
            return yearColumns;
        }

        //ANNUAL OVERVIEW
        // Months for annual overviews
        public static Dictionary<int, decimal> SetMonths()
        {
            Dictionary<int, decimal> HoursPerMonth = new Dictionary<int, decimal>();
            for (int i = 1; i <= 12; i++)
            {
                HoursPerMonth.Add(i, 0);
            }
            return HoursPerMonth;
        }

        public static void SetHourTypes(this Dictionary<string, decimal> hourTypes, List<DayType> dayTypes)
        {
            foreach (DayType day in dayTypes)
            {
                hourTypes.Add(day.Name, 0);
            }

            hourTypes.Add("Missing entries", 0);
        }

        public static void AddOvertime(this EmployeeTimeModel employeeTime, DayModel day)
        {        
            //Overtime isn't included in the TotalHours calculation
            if (day.TotalHours > 8)
            {
                employeeTime.Overtime += day.TotalHours - 8;
                employeeTime.TotalHours -= day.TotalHours - 8;
            }
            //Any weekend working hours will be added to overtime. Any weekend day that has tasks (working hours), is set to DayType "Workday"
            if (day.IsWeekend())
            {
                employeeTime.Overtime += day.TotalHours;
                employeeTime.TotalHours -= day.TotalHours;
            }
        }

        public static decimal CalculateOvertime(this List<DayModel> calendar)
        {
            decimal overtime = 0;
            List<DayModel> workdays = calendar.Where(x => x.IsWeekend() || x.DayType.Name == "Workday" && x.TotalHours > 8).ToList();
            foreach(DayModel day in calendar)
            {
                if (day.IsWeekend())
                {
                    overtime += day.TotalHours;

                }
                else if(day.IsWorkdayOvertime())
                {
                    overtime += day.TotalHours - 8;
                }
            }

            return overtime;
        }
    }
}
