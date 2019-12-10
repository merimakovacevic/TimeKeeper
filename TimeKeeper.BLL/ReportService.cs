using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        
        //THE COLUMN HEADERS AREN'T ORDERED AS THE CORRESPONDING HOURS THAT REPRESENT EACH COLUMN IN INDIVIDUAL ROWS!!!
       
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
