using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.BLL
{
    public class ReportService : CalendarService
    {
        protected List<DayType> _dayTypes;

        public ReportService(UnitOfWork unit) : base(unit)
        {
            _dayTypes = unit.DayTypes.Get().ToList();
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
                if (_dayTypes.FirstOrDefault(x => x.Name == day.DayType.Name) != null)
                {
                    /*The Get method in the generic Repository throws an exception if the entity isn't found, 
                     * so it is necessary to try to get the daytype from the database.*/

                    hours[day.DayType.Name] += day.TotalHours;
                    employeeReport.TotalHours += day.TotalHours;

                    //Is it better for this to be in a separate method, considering the application performance?
                    employeeReport.Overtime.AddOvertime(day);

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
        
        //THE COLUMN HEADERS AREN'T ORDERED AS THE CORRESPONDING HOURS THAT REPRESENT EACH COLUMN IN INDIVIDUAL ROWS!!!
        public MonthlyTimeModel GetMonthlyOverview(int year, int month)
        {
            MonthlyTimeModel pmm = new MonthlyTimeModel();

            var source = _unit.Tasks.Get(d => d.Day.Date.Year == year && d.Day.Date.Month == month).ToList();
            var tasks = source.OrderBy(x => x.Day.Employee.Id)
                              .ThenBy(x => x.Project.Id)
                              .ToList();

            pmm.Projects = tasks.GroupBy(p => new { p.Project.Id, p.Project.Name })
                                .Select(p => new MasterModel { Id = p.Key.Id, Name = p.Key.Name })
                                .ToList();
            List<int> pList = pmm.Projects.Select(p => p.Id).ToList();

            var details = tasks.GroupBy(x => new {
                empId = x.Day.Employee.Id,
                empName = x.Day.Employee.FullName,
                projId = x.Project.Id,
                projName = x.Project.Name
            })
                                            .Select(y => new
                                            {
                                                employee = new MasterModel { Id = y.Key.empId, Name = y.Key.empName },
                                                project = new MasterModel { Id = y.Key.projId, Name = y.Key.projName },
                                                hours = y.Sum(h => h.Hours)
                                            }).ToList();

            EmployeeProjectModel epm = new EmployeeProjectModel(pList) { Employee = new MasterModel { Id = 0 } };
            foreach (var item in details)
            {
                if (epm.Employee.Id != item.employee.Id)
                {
                    if (epm.Employee.Id != 0) pmm.Employees.Add(epm);
                    epm = new EmployeeProjectModel(pList) { Employee = item.employee };
                }
                epm.Hours[item.project.Id] += item.hours;
                epm.TotalHours += item.hours;
            }
            if (epm.Employee.Id != 0) pmm.Employees.Add(epm);

            return pmm;
        }

        /*
        public MonthlyOverviewModel GetMonthlyOverview(int year, int month)
        {
            MonthlyOverviewModel monthlyOverview = new MonthlyOverviewModel();
            //List<Project> projects = unit.Projects.Get().ToList().Where(x => x.StartDate.Year == year && x.StartDate.Month==month || 
            //                                                        x.EndDate.Year == year && x.EndDate.Month == month ||  
            //                                                        x.EndDate.Year <= year && x.StartDate.Year >= year).ToList();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            List<JobDetail> tasks = _unit.Tasks.Get().Where(x => x.Day.Date.Year == year && x.Day.Date.Month == month).ToList();
            //List<JobDetail> tasks= tasksAll.Where(x => x.Day.Date.Year == year && x.Day.Date.Month == month).ToList();
            List<Project> projects = new List<Project>();
            foreach (JobDetail task in tasks)
            {
                if (!projects.IsDuplicate(task.Project))
                {
                    projects.Add(task.Project);
                }
            }
            Dictionary<string, decimal> projectColumns = projects.SetMonthlyOverviewColumns();
            monthlyOverview.HoursByProject = projectColumns;
            monthlyOverview.TotalHours = 0;
            monthlyOverview.EmployeeProjectHours = new List<EmployeeProjectModel>();

            foreach (JobDetail task in tasks)
            {
                if (!employees.IsDuplicate(task.Day.Employee))
                {
                    employees.Add(task.Day.Employee.Create());
                }
            }
            foreach (EmployeeModel emp in employees)
            {
                List<JobDetail> employeesTasks = tasks.Where(x => x.Day.Employee.Id == emp.Id).ToList();
                EmployeeProjectModel employeeProjectModel = GetEmployeeMonthlyOverview(projects, emp, employeesTasks);
                foreach (KeyValuePair<string, decimal> keyValuePair in employeeProjectModel.HoursByProject)
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

        public EmployeeProjectModel GetEmployeeMonthlyOverview(List<Project> projects, EmployeeModel employee, List<JobDetail> tasks)
        {
            Dictionary<string, decimal> projectColumns = projects.SetMonthlyOverviewColumns();
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
        }*/
        
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
                    HoursPerYears = _unit.Projects.Get(projectId).SetYearsColumns(),
                    TotalHoursPerMonth = 0
                });
            }

            foreach (JobDetail task in employeeTasks)
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
                if (_unit.Employees.Get().ToList().FirstOrDefault(x => x.Id == a.Day.Employee.Id) != null && !employees.IsDuplicateEmployee(a.Day.Employee))
                {
                    employees.Add(_unit.Employees.Get(a.Day.Employee.Id));
                }
            }

            foreach (Employee emp in employees)
            {
                EmployeeProjectHistoryModel e = new EmployeeProjectHistoryModel
                {
                    EmployeeName = emp.FullName,
                    HoursPerYears = _unit.Projects.Get(projectId).SetYearsColumns(),
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
            projectHistory.TotalYearlyProjectHours = _unit.Projects.Get(projectId).SetYearsColumns();

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
        public List<AnnualTimeModel> GetAnnualOverview(int year)
        {
            List<AnnualTimeModel> result = new List<AnnualTimeModel>();
            AnnualTimeModel total = new AnnualTimeModel { Project = new MasterModel { Id = 0, Name = "TOTAL" } };
            List<Project> projects = _unit.Projects.Get().OrderBy(p => p.Name).ToList();
            foreach (Project p in projects)
            {
                List<JobDetail> query = p.Tasks.Where(d => d.Day.Date.Year == year).ToList();
                if (query.Count != 0)
                {
                    var list = query.GroupBy(d => d.Day.Date.Month)
                                    .Select(x => new { month = x.Key, hours = x.Sum(y => y.Hours) });
                    AnnualTimeModel atm = new AnnualTimeModel { Project = p.Master() };
                    foreach (var item in list)
                    {
                        atm.Hours[item.month - 1] = item.hours;
                        atm.Total += item.hours;

                        total.Hours[item.month - 1] += item.hours;
                        total.Total += item.hours;
                    }
                    total.Project.Id++;
                    result.Add(atm);
                }
            }
            result.Add(total);
            return result;
        }

        /*
        public ProjectAnnualOverviewModel GetProjectAnnualOverview(int projectId, int year)
        {
            // Arrange
            var project = _unit.Projects.Get(projectId);
            var tasksOnProject = _unit.Tasks.Get().Where(x => x.Project.Id == projectId && x.Day.Date.Year == year).ToList();
            ProjectAnnualOverviewModel projectAnnualOverview = new ProjectAnnualOverviewModel { Project = project.Master() };
            // Fill
            projectAnnualOverview.Months = StaticHelper.SetMonths();
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
            totalAnnualOverview.Months = StaticHelper.SetMonths();
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
        }*/
    }
}
