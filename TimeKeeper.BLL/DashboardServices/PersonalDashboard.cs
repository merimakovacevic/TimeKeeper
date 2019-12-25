using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL;
using TimeKeeper.DTO;
using TimeKeeper.DTO.ReportModels.PersonalDashboard;
using TimeKeeper.DTO.ReportModels.TeamDashboard;

namespace TimeKeeper.BLL.DashboardServices
{
    public class PersonalDashboard : CalendarService
    {
        protected StoredProcedureService _storedProcedureService;
        public PersonalDashboard(UnitOfWork unit): base(unit)
        {
            _storedProcedureService = new StoredProcedureService(unit);
        }

        public PersonalDashboardStoredModel GetPersonalDashboardStored(int empId, int year, int month)
        {
            PersonalDashboardStoredModel personalDashboard = new PersonalDashboardStoredModel();
            List<PersonalDashboardRawModel> rawData = _storedProcedureService.GetStoredProcedure<PersonalDashboardRawModel>("personalDashboard", new int[] { empId, year, month });
            decimal workingDaysInMonth = GetMonthlyWorkingDays(year, month) * 8;
            decimal workingDaysInYear = GetYearlyWorkingDays(year) * 8;
            personalDashboard.PersonalDashboardHours = rawData[0];
            // What if there's overtime?
            personalDashboard.UtilizationMonthly = decimal.Round(((rawData[0].WorkingMonthly / workingDaysInMonth) * 100), 2, MidpointRounding.AwayFromZero);
            personalDashboard.UtilizationYearly = decimal.Round(((rawData[0].WorkingYearly / workingDaysInYear) * 100), 2, MidpointRounding.AwayFromZero);
            personalDashboard.BradfordFactor = GetBradfordFactor(rawData[0].EmployeeId, year);
            return personalDashboard;
        }

        public decimal GetBradfordFactor(PersonalDashboardRawModel personalDashboardHours, int year)
        {
            
            int absenceDays = personalDashboardHours.SickYearly;

            List<TeamRawCountModel> rawDataCount = _storedProcedureService.GetStoredProcedure<TeamRawCountModel>("sickByMonths", new int[] { personalDashboardHours.EmployeeId, year});
            int absenceInstances = rawDataCount.Count;


            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from sickByMonths({personalDashboardHours.EmployeeId}, {year})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            if (sql.HasRows)
            {
                while (sql.Read())
                {
                    absenceInstances++;
                }
            }
            return (decimal)Math.Pow(absenceInstances, 2) * absenceDays;
        }
    }
}
