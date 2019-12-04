using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;

namespace TimeKeeper.BLL
{
    public static class StaticHelper
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

        public static void AddOvertime(this decimal overtime, DayModel day)
        {
            if (day.DayType.Name == "Workday" && day.TotalHours > 8)
            {
                overtime += day.TotalHours - 8;
            }
            //Any weekend working hours will be added to overtime. Any weekend day that has tasks (working hours), is set to DayType "Workday"
            if (day.DayType.Name == "Workday" && day.IsWeekend())
            {
                overtime += day.TotalHours;
            }
        }
    }
}
