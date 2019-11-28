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

        public TeamDashboardModel GetTeamDashboard(int teamId, int year, int month)
        {

            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = GetTeamMonthReport(teamId, year, month)
            };

            //projects for this month!!!
            teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
            teamDashboard.ProjectsCount = unit.Teams.Get(teamId).Projects.Count();

            foreach(EmployeeTimeModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.WorkingHours += employeeTime.HourTypes["Workday"];
            }

            return teamDashboard;
        }

        public List<EmployeeTimeModel> GetTeamMonthReport(int teamId, int year, int month)
        {
            //Show only team members that were active during this month
            //filter total hours > 0?
            Team team = unit.Teams.Get(teamId);
            List<EmployeeTimeModel> employeeTimeModels = new List<EmployeeTimeModel>();

            foreach (Member member in team.Members)
            {
                employeeTimeModels.Add(GetEmployeeMonthReport(member.Employee.Id, year, month));
            }
            return employeeTimeModels;
        }

        public PersonalDashboardModel GetEmployeeYearDashboard (int employeeId, int year)
        {
            EmployeeTimeModel employeeYearTime = unit.Employees.Get(employeeId).CreateTimeModel();
            employeeYearTime.HourTypes.SetHourTypes(unit);

            for (int i = 1; i <= 12; i++)
            {
                EmployeeTimeModel employeeMonthTime = GetEmployeeMonthReport(employeeId, year, i);
                foreach(KeyValuePair<string, decimal> hourType in employeeMonthTime.HourTypes)
                {
                    employeeYearTime.HourTypes[hourType.Key] += hourType.Value;
                }
                employeeYearTime.Overtime += employeeMonthTime.Overtime;
                employeeYearTime.PaidTimeOff += employeeMonthTime.PaidTimeOff;
            }

            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);

            PersonalDashboardModel personalDashboard = new PersonalDashboardModel
            {
                Employee = employeeYearTime.Employee,
                TotalHours = GetYearlyWorkingDays(year) * 8,
                WorkingHours = employeeYearTime.HourTypes["Workday"],
                BradfordFactor = GetBradfordFactor(calendar)
            };

            return personalDashboard;
        }
               
        public PersonalDashboardModel GetEmployeeMonthDashboard(int employeeId, int year, int month)
        {
            EmployeeTimeModel employeeTime = GetEmployeeMonthReport(employeeId, year, month);
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year, month);
            //unit.Employees.Get(employeeId).Calendar.Select(x => x.Create()).ToList();

            PersonalDashboardModel personalDashboard = new PersonalDashboardModel
            {
                Employee = employeeTime.Employee,
                TotalHours = GetMonthlyWorkingDays(year, month) * 8,
                WorkingHours = employeeTime.HourTypes["Workday"],
                BradfordFactor = GetBradfordFactor(calendar)
            };

            return personalDashboard;
        }
        
        public EmployeeTimeModel GetEmployeeMonthReport(int employeeId, int year, int month)
        {
            //TOTAL HOURS IN DASHBOARD - MONTHLY THEORETICAL WORKING HOURS
            Employee employee = unit.Employees.Get(employeeId);
            EmployeeTimeModel employeeReport = employee.CreateTimeModel();
            List<DayModel> calendar = GetEmployeeMonth(employeeId, year, month);

            employeeReport.HourTypes.SetHourTypes(unit);

            //The SetHourTypes function was moved to TimeKeeper.Api.Services.Services
            //SetHourTypes(employeeReport.HourTypes);
            Dictionary<string, decimal> hours = employeeReport.HourTypes;//this is to shorten down the Dictionary name

            foreach (DayModel day in calendar)
            {
                //the adding operations below will only be performed upon database DayTypes, the in memory types will be omitted
                try
                {
                    /*The Get method in the generic Repository throws an exception if the entity isn't found, 
                     * so it is necessary to try to get the daytype from the database.
                     *This could have been done using a simpler If statement, but it wouldn't be 
                     * as reliable considering the possible key and value changes of all DayType values */
                    unit.DayTypes.Get(day.DayType.Id);
                    hours[day.DayType.Name] += day.TotalHours;
                    employeeReport.TotalHours += day.TotalHours;

                    if (day.DayType.Name == "Workday" && day.TotalHours > 8)
                    {
                        employeeReport.Overtime += day.TotalHours - 8;
                    }
                    //Any weekend working hours will be added to overtime. Any weekend day that has tasks (working hours), is set to DayType "Workday"
                    if (day.DayType.Name == "Workday" && day.IsWeekend())
                    {
                        employeeReport.Overtime += day.TotalHours;
                    }
                    /*if the total recorded hours for a Workday are less than 8, the difference is added to the missing entries*/
                    /*If tasks are added to weekend day, the day is saved as a workday. In that case, it is not necessary to add
                     the difference to the missing entries*/
                    if (day.TotalHours < 8 && !day.IsWeekend()) //is this necessary? SHOULD WE DELETE IT?
                    {
                        hours["Missing entries"] += 8 - day.TotalHours;
                    }
                    //Any additional day types to be added as paid time off? Other (Id = 7)?
                    if (day.DayType.Name != "Workday")
                    {
                        employeeReport.PaidTimeOff += day.TotalHours;
                    }
                }
                catch(Exception ex) { }            
            }
            
            hours["Missing entries"] = calendar.FindAll(x => x.DayType.Name == "Empty").Count() * 8;
            //Missing entries are included in the Total hours sum
            employeeReport.TotalHours += hours["Missing entries"];
           
            return employeeReport;
        }

        public List<DayModel> GetEmployeeMonth(int empId, int year, int month)
        {
            List<DayModel> calendar = GetEmptyEmployeeCalendar(empId, year, month);
            List<DayModel> employeeDays = GetEmployeeCalendar(empId, year, month);

            foreach (var d in employeeDays)
            {
                calendar[d.Date.Day - 1] = d;
            }
            return calendar;
        }
        
        public List<DayModel> GetEmployeeCalendar(int empId, int year)
        {
            //Add validaiton!
            return unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year).Select(x => x.Create()).ToList();
        }

        public List<DayModel> GetEmployeeCalendar(int empId, int year, int month)
        {
            //Add validaiton!
            return unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).ToList();
        }

        public List<DayModel> GetEmptyEmployeeCalendar(int empId, int year, int month)
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

                if (day.IsWeekend()) newDay.DayType = weekend.Master();
                if (day > DateTime.Today) newDay.DayType = future.Master();
                if (day < emp.BeginDate || (emp.EndDate != new DateTime(1, 1, 1) && emp.EndDate != null && day > emp.EndDate)) newDay.DayType = na.Master();

                calendar.Add(newDay);
                day = day.AddDays(1);
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

        public decimal GetBradfordFactor(List<DayModel> calendar)
        {   
            //an absence instance are any number of consecutive absence days. 3 consecutive absence days make an instance.
            int absenceInstances = 0;
            int absenceDays = 0;
            calendar = calendar.OrderBy(x => x.Date).ToList();
            
            //Bradford factor calculates only dates until the rpesent day, because the calendar in argument returns the whole period
            absenceDays = calendar.Where(x => x.IsAbsence() && x.Date < DateTime.Now).Count();

            for (int i = 0; i < calendar.Count; i++)
            {
                if (calendar[i].IsAbsence() && calendar[i].Date < DateTime.Now)
                {
                    if(i == 0) absenceInstances++;

                    else if(!calendar[i-1].IsAbsence())
                    {
                        absenceInstances++;
                    }
                }
            }
            return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
        }

        private int GetYearlyWorkingDays(int year)
        {
            int workingDays = 0;
            for (int i = 1; i <= 12; i++)
            {
                workingDays += GetMonthlyWorkingDays(year, i);
            }

            return workingDays;
        }


        private int GetMonthlyWorkingDays(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workingDays = 0;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime thisDay = new DateTime(year, month, i);
                if (!thisDay.IsWeekend() && thisDay < DateTime.Now)
                {
                    workingDays++;
                }
            }

            return workingDays;
        }

        private decimal GetMonthlyWorkingDays(Employee employee, int year, int month)
        {
            //this method counts only days until the present day
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workingDays = 0;

            for (int i = 1; i <= daysInMonth; i++)
            {
                DateTime thisDay = new DateTime(year, month, i);
                if (!thisDay.IsWeekend() && thisDay < DateTime.Now && employee.BeginDate < thisDay && (employee.EndDate != new DateTime(1, 1, 1) && employee.EndDate != null && thisDay > employee.EndDate))
                {
                    workingDays++;
                }
            }

            return workingDays;
        }

        public MonthlyOverviewModel GetMonthlyOverview(int year, int month)
        {
            MonthlyOverviewModel monthlyOverview = new MonthlyOverviewModel();
            //List<Project> projects = unit.Projects.Get().ToList().Where(x => x.StartDate.Year == year && x.StartDate.Month==month || 
            //                                                        x.EndDate.Year == year && x.EndDate.Month == month ||  
            //                                                        x.EndDate.Year <= year && x.StartDate.Year >= year).ToList();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            List<JobDetail> tasks = unit.Tasks.Get().Where(x => x.Day.Date.Year==year && x.Day.Date.Month==month).ToList();
            //List<JobDetail> tasks= tasksAll.Where(x => x.Day.Date.Year == year && x.Day.Date.Month == month).ToList();
            List<Project> projects = new List<Project>();
            foreach(JobDetail task in tasks)
            {
                if (!IsDuplicate(projects, task.Project))
                {
                    projects.Add(task.Project);
                }
            }
            Dictionary<string, decimal> projectColumns = SetMonthlyOverviewColumns(projects);
            monthlyOverview.HoursByProject = projectColumns;
            monthlyOverview.TotalHours = 0;
            monthlyOverview.EmployeeProjectHours = new List<EmployeeProjectModel>();

            foreach (JobDetail task in tasks)
            {
                if (!IsDuplicate(employees, task.Day.Employee)) {
                    employees.Add(task.Day.Employee.Create());
                }
            }
            foreach(EmployeeModel emp in employees)
            {
                List<JobDetail> employeesTasks = tasks.Where(x => x.Day.Employee.Id == emp.Id).ToList();
                EmployeeProjectModel employeeProjectModel = GetEmployeeMonthlyOverview(projects, emp, employeesTasks);
                foreach(KeyValuePair<string, decimal> keyValuePair in employeeProjectModel.HoursByProject)
                {
                    monthlyOverview.HoursByProject[keyValuePair.Key] += keyValuePair.Value;
                }
                monthlyOverview.EmployeeProjectHours.Add(employeeProjectModel);
                monthlyOverview.TotalHours += employeeProjectModel.TotalHours;
            }
            monthlyOverview.TotalWorkingDays = GetMonthlyWorkingDays(year, month);
            monthlyOverview.TotalPossibleWorkingHours = monthlyOverview.TotalWorkingDays * 8;
            return monthlyOverview;

        }

        public bool IsDuplicate(List<EmployeeModel> employees, Employee employee)
        {
            foreach(EmployeeModel emp in employees)
            {
                if (emp.Id == employee.Id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsDuplicate(List<Project> projects, Project project)
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

        public EmployeeProjectModel GetEmployeeMonthlyOverview(List<Project> projects, EmployeeModel employee, List<JobDetail> tasks)
        {
            Dictionary<string, decimal> projectColumns = SetMonthlyOverviewColumns(projects);
            EmployeeProjectModel employeeProject = new EmployeeProjectModel();
            employeeProject.Employee = unit.Employees.Get(employee.Id).Master();
            employeeProject.HoursByProject = projectColumns;
            employeeProject.TotalHours = 0;
            employeeProject.PaidTimeOff = 0;
            foreach (JobDetail task in tasks)
            {
                employeeProject.HoursByProject[task.Project.Name] += task.Hours;
                employeeProject.TotalHours += task.Hours;
                if (task.Day.IsAbsence()) employeeProject.PaidTimeOff += 8;
            }
            return employeeProject;
        }

        public Dictionary<string, decimal> SetMonthlyOverviewColumns(List<Project> projects)
        {
            Dictionary<string, decimal> projectColumns = new Dictionary<string, decimal>(); 
            foreach(Project project in projects)
            {
                projectColumns.Add(project.Name, 0);
            }
            return projectColumns;
        }
    }
}
