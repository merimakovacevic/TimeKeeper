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
using TimeKeeper.Utility.Factory;

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
            if(rawData.Count > 0)
            {
                personalDashboard.PersonalDashboardHours = rawData[0];
                // What if there's overtime?
                personalDashboard.UtilizationMonthly = decimal.Round(((rawData[0].WorkingMonthly / workingDaysInMonth) * 100), 2, MidpointRounding.AwayFromZero);
                personalDashboard.UtilizationYearly = decimal.Round(((rawData[0].WorkingYearly / workingDaysInYear) * 100), 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                EmployeeModel employee = _unit.Employees.Get(empId).Create();
                personalDashboard.PersonalDashboardHours = new PersonalDashboardRawModel { EmployeeId = employee.Id, EmployeeName = employee.FullName };
            }
            personalDashboard.BradfordFactor = GetBradfordFactor(empId, year);
            return personalDashboard;
        }
    }
}
