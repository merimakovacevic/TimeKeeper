﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.BLL
{
    public class DashboardService : CalendarService
    {
        protected QueryService _queryService;
        protected ReportService _reportService;

        public DashboardService(UnitOfWork unit) : base(unit)
        {
            _queryService = new QueryService(unit);
            _reportService = new ReportService(unit);
        }
        public AdminDashboardModel GetAdminDashboardInfo(int year, int month)
        {
            //Is the missing entries chart in Admin dashboard referring to missing entries per team or?

            AdminDashboardModel adminDashboardModel = new AdminDashboardModel();
            //no of employees at current month/year
            adminDashboardModel.EmployeesCount = _queryService.GetNumberOfEmployeesForTimePeriod(month, year);
            //no of projects in a current month/year
            adminDashboardModel.ProjectsCount = _queryService.GetNumberOfProjectsForTimePeriod(month, year);
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

        public TeamDashboardModel GetTeamDashboard(int teamId, int year, int month)
        {
            //The DashboardService shouldn't really depend on the report service, this should be handled in another way
            TeamDashboardModel teamDashboard = new TeamDashboardModel
            {
                EmployeeTimes = _reportService.GetTeamMonthReport(teamId, year, month)
            };

            //projects for this month!!!
            teamDashboard.EmployeesCount = teamDashboard.EmployeeTimes.Count();
            teamDashboard.ProjectsCount = _unit.Teams.Get(teamId).Projects.Count();

            foreach (EmployeeTimeModel employeeTime in teamDashboard.EmployeeTimes)
            {
                teamDashboard.TotalHours += employeeTime.TotalHours;
                teamDashboard.TotalWorkingHours += employeeTime.HourTypes["Workday"];
                teamDashboard.TotalMissingEntries += employeeTime.HourTypes["Missing entries"];
            }

            return teamDashboard;
        }

        public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year)
        {
            List<DayModel> calendar = GetEmployeeCalendar(employeeId, year);
            decimal totalHours = GetYearlyWorkingDays(year) * 8;

            return CreatePersonalDashboard(employeeId, year, totalHours, calendar);
        }
        public PersonalDashboardModel GetEmployeeDashboard(int employeeId, int year, int month)
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
                    if (i == 0) absenceInstances++;

                    else if (calendar[i - 1].DayType.Name != "Sick")
                    {
                        absenceInstances++;
                    }
                }
            }
            return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
        }

    }
}
