using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.API.Models.ReportModels;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Services
{
    public class CalendarService
    {
        protected UnitOfWork _unit;
        protected List<DayType> _dayTypes;

        public CalendarService(UnitOfWork unit)
        {
            _unit = unit;
            _dayTypes = unit.DayTypes.Get().ToList();
        }

        public AdminDashboardModel GetAdminDashboardInfo(int year, int month)
        {
            //Is the missing entries chart in Admin dashboard referring to missing entries per team or?

            AdminDashboardModel adminDashboardModel = new AdminDashboardModel();
            //no of employees at current month/year
            adminDashboardModel.EmployeesCount = GetNumberOfEmployeesForTimePeriod(month, year);
            //no of projects in a current month/year
            adminDashboardModel.ProjectsCount = GetNumberOfProjectsForTimePeriod(month, year);
            //total hours; what is total hours?
            //adminDashboardModel.BaseTotalHours = GetMonthlyWorkingDays(year, month) * 8;
            decimal monthlyBaseHours = GetMonthlyWorkingDays(year, month) * 8;
            adminDashboardModel.TotalHours = monthlyBaseHours * adminDashboardModel.EmployeesCount;
            adminDashboardModel.TotalWorkingHours = 0;

            List<int> teamIds = _unit.Teams.Get().Select(x => x.Id).ToList();

            foreach (int teamId in teamIds)
            {
                MasterModel team = _unit.Teams.Get(teamId).Master();
                TeamDashboardModel teamDashboardModel = GetTeamDashboard(teamId, year, month);
                adminDashboardModel.TeamDashboardModels.Add(teamDashboardModel);
                adminDashboardModel.TotalWorkingHours += teamDashboardModel.TotalWorkingHours;
                /*
                //missing entries by team
                adminDashboardModel.MissingEntries.Add(new TeamKeyDictionary(team, teamDashboardModel.MissingEntries.Sum(x => x.Value)));
                //pto hours by team
                adminDashboardModel.PTOHours.Add(new TeamKeyDictionary(team, teamDashboardModel.PaidTimeOff.Sum(x => x.Value)));
                //overtime horus by team
                adminDashboardModel.OvertimeHours.Add(new TeamKeyDictionary(team, teamDashboardModel.Overtime.Sum(x => x.Value)));*/
            }
            //what is considered by utilization? :thinkig-face:
            return adminDashboardModel;
        }
        public int GetNumberOfEmployeesForTimePeriod(int month, int year)
        {
            return _unit.Employees.Get(x => x.BeginDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if employees begin date is in required month
                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // still works in company, or left company after required month          
        }
        public int GetNumberOfProjectsForTimePeriod(int month, int year)
        {
            return _unit.Projects.Get(x => x.StartDate < new DateTime(year, month, DateTime.DaysInMonth(year, month)) //if project began is in required month
                            && (x.EndDate == null || x.EndDate == new DateTime(1, 1, 1) || x.EndDate > new DateTime(year, month, DateTime.DaysInMonth(year, month)))).Count(); // project still in progress, or ended after the required month          
        }
        public TeamDashboardModel GetTeamDashboard(int teamId, int year, int month)
        {

            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = GetTeamMonthReport(teamId, year, month)
            };

            //projects for this month!!!
            teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
            teamDashboard.ProjectsCount = _unit.Teams.Get(teamId).Projects.Count();

            foreach(EmployeeTimeModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.TotalWorkingHours += employeeTime.HourTypes["Workday"];
                teamDashboard.TotalMissingEntries += employeeTime.HourTypes["Missing entries"];
            }

            return teamDashboard;
        }
        public List<EmployeeTimeModel> GetTeamMonthReport(int teamId, int year, int month)
        {
            //Show only team members that were active during this month
            //filter total hours > 0?
            Team team = _unit.Teams.Get(teamId);
            List<EmployeeTimeModel> employeeTimeModels = new List<EmployeeTimeModel>();

            foreach (Member member in team.Members)
            {
                employeeTimeModels.Add(GetEmployeeMonthReport(member.Employee.Id, year, month));
            }
            return employeeTimeModels;
        }
        public PersonalDashboardModel GetEmployeeYearDashboard(int employeeId, int year)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
            decimal totalHours = GetYearlyWorkingDays(year) * 8;

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }               
        public PersonalDashboardModel GetEmployeeMonthDashboard(int employeeId, int year, int month)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year, month);            
            decimal totalHours = GetMonthlyWorkingDays(year, month) * 8;

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }            
        private PersonalDashboardModel CreatePersonalDashboard(int employeeId, int year, decimal totalHours, List<DayModel> calendar)
        {
            decimal workingHours = calendar.Where(x => x.DayType.Name == "Workday").Sum(x => x.TotalHours);

            return new PersonalDashboardModel
            {
                Employee = _unit.Employees.Get(employeeId).Master(),
                TotalHours = totalHours,
                WorkingHours = workingHours,
                BradfordFactor = GetBradfordFactor(employeeId, year)
            };
        }
        public EmployeeTimeModel GetEmployeeMonthReport(int employeeId, int year, int month)
        {
            //TOTAL HOURS IN DASHBOARD - MONTHLY THEORETICAL WORKING HOURS
            Employee employee = _unit.Employees.Get(employeeId);
            EmployeeTimeModel employeeReport = employee.CreateTimeModel();
            List<DayModel> calendar = GetEmployeeMonth(employeeId, year, month);

            employeeReport.HourTypes.SetHourTypes(_dayTypes);

            //this is to shorten down the Dictionary name
            Dictionary<string, decimal> hours = employeeReport.HourTypes;

            foreach (DayModel day in calendar)
            {
                if (day.DayType.Name == "Empty") hours["Missing entries"] += 8;

                //the adding operations below will only be performed upon database DayTypes, the in memory types will be omitted
                if(_dayTypes.FirstOrDefault(x => x.Name == day.DayType.Name) != null)
                {
                    /*The Get method in the generic Repository throws an exception if the entity isn't found, 
                     * so it is necessary to try to get the daytype from the database.*/

                    hours[day.DayType.Name] += day.TotalHours;
                    employeeReport.TotalHours += day.TotalHours;

                    //Is it better for this to be in a separate method, considering the application performance?
                    employeeReport.Overtime += AddOvertime(day);

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
            }
            
            //hours["Missing entries"] = calendar.FindAll(x => x.DayType.Name == "Empty").Count() * 8;
            //Missing entries are included in the Total hours sum
            employeeReport.TotalHours += hours["Missing entries"];
           
            return employeeReport;
        }
        private decimal AddOvertime(DayModel day)
        {
            decimal overtime = 0;
            if (day.DayType.Name == "Workday" && day.TotalHours > 8)
            {
                overtime += day.TotalHours - 8;
            }
            //Any weekend working hours will be added to overtime. Any weekend day that has tasks (working hours), is set to DayType "Workday"
            if (day.DayType.Name == "Workday" && day.IsWeekend())
            {
                overtime += day.TotalHours;
            }

            return overtime;
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

        public List<DayModel> GetEmployeeCalendar(int employeeId, int year)
        {
            //Add validaiton!
            return _unit.Calendar.Get(x => x.Employee.Id == employeeId && x.Date.Year == year).Select(x => x.Create()).ToList();
        }
        public List<DayModel> GetEmployeeCalendar(int empId, int year, int month)
        {
            //Add validaiton!
            return _unit.Calendar.Get(x => x.Employee.Id == empId && x.Date.Year == year && x.Date.Month == month).Select(x => x.Create()).ToList();
        }
        public List<DayModel> GetEmptyEmployeeCalendar(int employeeId, int year, int month)
        {
            List<DayModel> calendar = new List<DayModel>();
            if (!ValidateMonth(year, month)) throw new Exception("Invalid data! Check year and month");

            DayType future = new DayType { Id = 10, Name = "Future" };
            DayType empty = new DayType { Id = 11, Name = "Empty" };
            DayType weekend = new DayType { Id = 12, Name = "Weekend" };
            DayType na = new DayType { Id = 13, Name = "N/A" };

            DateTime day = new DateTime(year, month, 1);
            Employee emp = _unit.Employees.Get(employeeId);
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
        public decimal GetBradfordFactor(int employeeId, int year)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
            //an absence instance are any number of consecutive absence days. 3 consecutive absence days make an instance.
            int absenceInstances = 0;
            int absenceDays = 0;
            calendar = calendar.OrderBy(x => x.Date).ToList();
            
            //Bradford factor calculates only dates until the present day, because the calendar in argument returns the whole period
            absenceDays = calendar.Where(x => x.DayType.Name == "Sick" && x.Date < DateTime.Now).Count();

            for (int i = 0; i < calendar.Count; i++)
            {
                if (calendar[i].DayType.Name == "Sick" && calendar[i].Date < DateTime.Now)
                {
                    if(i == 0) absenceInstances++;

                    else if(calendar[i-1].DayType.Name != "Sick")
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
        private int GetMonthlyWorkingDays(Employee employee, int year, int month)
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
            List<JobDetail> tasks = _unit.Tasks.Get().Where(x => x.Day.Date.Year==year && x.Day.Date.Month==month).ToList();
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
            employeeProject.Employee = _unit.Employees.Get(employee.Id).Master();
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
        //PROJECT HISTORY
        public Dictionary<int, decimal> SetYearsColumns(int projectId)
        {
            Dictionary<int, decimal> yearColumns = new Dictionary<int, decimal>();
            List<JobDetail> tasks = _unit.Projects.Get(projectId).Tasks.ToList();
            foreach (JobDetail a in tasks)
            {
                if (!IsDuplicateYear(yearColumns, a.Day.Date.Year))
                    yearColumns.Add(a.Day.Date.Year, 0);
            }
            return yearColumns;
        }
        public bool IsDuplicateEmployee(List<Employee> employees, Employee employee)
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
        public bool IsDuplicateYear(Dictionary<int, decimal> yearColumns, int year)
        {
            if (yearColumns.ContainsKey(year)) {
                return true;
                }
            return false;
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
                    HoursPerYears = SetYearsColumns(projectId),
                    TotalHoursPerMonth = 0
                });    
            }

            foreach(JobDetail task in employeeTasks)
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
                if (_unit.Employees.Get().ToList().FirstOrDefault(x => x.Id == a.Day.Employee.Id) != null && !IsDuplicateEmployee(employees, a.Day.Employee))
                {
                    employees.Add(_unit.Employees.Get(a.Day.Employee.Id));
                }
            }

            foreach (Employee emp in employees)
            {
                EmployeeProjectHistoryModel e = new EmployeeProjectHistoryModel
                {
                    EmployeeName = emp.FullName,
                    HoursPerYears = SetYearsColumns(projectId),
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
            projectHistory.TotalYearlyProjectHours = SetYearsColumns(projectId);
            
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
        //ANNUAL OVERVIEW
        // Months for annual overviews
        public Dictionary<int, decimal> SetMonths()
        {
            Dictionary<int, decimal> HoursPerMonth = new Dictionary<int, decimal>();
            for (int i = 1; i <= 12; i++)
            {
                HoursPerMonth.Add(i, 0);
            }
            return HoursPerMonth;
        }
        public ProjectAnnualOverviewModel GetProjectAnnualOverview(int projectId, int year)
        {
            // Arrange
            var project = _unit.Projects.Get(projectId);
            var tasksOnProject = _unit.Tasks.Get().Where(x => x.Project.Id == projectId && x.Day.Date.Year == year).ToList();
            ProjectAnnualOverviewModel projectAnnualOverview = new ProjectAnnualOverviewModel { Project = project.Master() };
            // Fill
            projectAnnualOverview.Months = SetMonths();
            foreach (var TaskOnProject in tasksOnProject)
            {
                projectAnnualOverview.Months[TaskOnProject.Day.Date.Month] += TaskOnProject.Hours;
                projectAnnualOverview.Total += TaskOnProject.Hours;
            }
            // Return
            return projectAnnualOverview;
        }
        public TotalAnnualOverviewModel GetTotalAnnualOverview(int year)
        {
            // Arrange
            TotalAnnualOverviewModel totalAnnualOverview = new TotalAnnualOverviewModel();
            List<ProjectAnnualOverviewModel> annualList = new List<ProjectAnnualOverviewModel>();
            var projectsInYear = _unit.Projects.Get().ToList();
            totalAnnualOverview.Months = SetMonths();
            // Fill
            foreach (Project projectInYear in projectsInYear)
            {
                var tasksInYear = _unit.Tasks.Get().Where(x => x.Project.Id == projectInYear.Id && x.Day.Date.Year == year).ToList();
                ProjectAnnualOverviewModel projectOverview = GetProjectAnnualOverview(projectInYear.Id, year);
                annualList.Add(projectOverview);
                totalAnnualOverview.Projects = annualList.ToList();
                foreach (var taskInYear in tasksInYear)
                {
                    totalAnnualOverview.Months[taskInYear.Day.Date.Month] += taskInYear.Hours;
                    totalAnnualOverview.SumTotal += taskInYear.Hours;
                }
            }
            // Return
            return totalAnnualOverview;
        }
    }
}
