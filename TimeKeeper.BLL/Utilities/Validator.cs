using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;

namespace TimeKeeper.BLL.Utilities
{
    public static class Validator
    {
        public static bool ValidateMonth(int year, int month)
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
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsWeekend(this DayModel day)
        {
            return day.Date.IsWeekend();
        }
        public static bool IsWorkdayOvertime(this DayModel day)
        {
            return day.IsWorkday() && day.TotalHours > 8;
        }
        public static bool IsWorkday(this DayModel day)
        {
            return day.DayType.Name == "Workday";
        }

        public static bool IsDateInPeriod(this Day day, int year, int month)
        {
            return day.Date.Year == year && day.Date.Month == month;
        }

        public static bool IsAbsence(this DayModel day)
        {
            return day.DayType.Name != "Workday";
        }

        public static bool IsAbsence(this Day day)
        {
            return day.DayType.Name != "Workday";
        }
        public static bool IsDuplicateEmployee(this List<Employee> employees, Employee employee)
        {
            if (employees.Count == 0) return false;
            foreach (Employee emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsDuplicateYear(this Dictionary<int, decimal> yearColumns, int year)
        {
            if (yearColumns.ContainsKey(year))
            {
                return true;
            }
            return false;
        }

        public static bool IsDuplicate(this List<EmployeeModel> employees, Employee employee)
        {
            foreach (EmployeeModel emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsDuplicate(this List<Project> projects, Project project)
        {
            foreach (Project proj in projects)
            {
                if (proj.Id == project.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
