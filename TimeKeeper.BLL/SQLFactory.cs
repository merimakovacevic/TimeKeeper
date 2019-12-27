using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using TimeKeeper.DTO.ReportModels.AnnualOverview;
using TimeKeeper.DTO.ReportModels.CompanyDashboard;
using TimeKeeper.DTO.ReportModels.PersonalDashboard;
using TimeKeeper.DTO.ReportModels.TeamDashboard;

namespace TimeKeeper.BLL
{
    public class SQLFactory
    {
        public List<Entity> BuildSQL<Entity>(DbDataReader sql)
        {
            if (typeof(Entity) == typeof(CompanyDashboardRawModel)) return CreateCompanyRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AnnualRawModel)) return CreateAnnualRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(CompanyRolesRawModel)) return CreateCompanyRoles(sql) as List<Entity>;
            if (typeof(Entity) == typeof(CompanyOvertimeModel)) return CreateCompanyOvertime(sql) as List<Entity>;
            if (typeof(Entity) == typeof(CompanyEmployeeHoursModel)) return CreateEmployeeHours(sql) as List<Entity>;
            if (typeof(Entity) == typeof(PersonalDashboardRawModel)) return CreatePersonalRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(TeamRawModel)) return CreateTeamRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(TeamRawNonWorkingHoursModel)) return CreateTeamRawNonWorkingHours(sql) as List<Entity>;
            if (typeof(Entity) == typeof(TeamRawCountModel)) return TeamRawCountModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(DateTime)) return CreateSickDaysList(sql) as List<Entity>;
            else return null;
        }

        private List<DateTime> CreateSickDaysList(DbDataReader sql)
        {
            List<DateTime> rawData = new List<DateTime>();
            while (sql.Read())
            {
                DateTime date = sql.GetDateTime(0);
                rawData.Add(date);
            }
            return rawData;
        }

        public List<TeamRawCountModel> TeamRawCountModel(DbDataReader sql)
        {
            List<TeamRawCountModel> rawData = new List<TeamRawCountModel>();
            while (sql.Read())
            {
                rawData.Add(new TeamRawCountModel
                {
                    ProjectId = sql.GetInt32(0)
                });
            }
            return rawData;
        }

        private List<TeamRawNonWorkingHoursModel> CreateTeamRawNonWorkingHours(DbDataReader sql)
        {
            List<TeamRawNonWorkingHoursModel> rawData = new List<TeamRawNonWorkingHoursModel>();
            while (sql.Read())
            {
                rawData.Add(new TeamRawNonWorkingHoursModel
                {
                    MemberId = sql.GetInt32(0),
                    Value = sql.GetDecimal(1)
                });
            }
            return rawData;
        }

        private List<TeamRawModel> CreateTeamRawModel(DbDataReader sql)
        {
            List<TeamRawModel> rawData = new List<TeamRawModel>();
            while (sql.Read())
            {
                rawData.Add(new TeamRawModel
                {
                    EmployeeId = sql.GetInt32(0),
                    EmployeeName = sql.GetString(1),
                    Value = sql.GetDecimal(2)
                });
            }
            return rawData;
        }

        private List<PersonalDashboardRawModel> CreatePersonalRawModel(DbDataReader sql)
        {
            List<PersonalDashboardRawModel> rawData = new List<PersonalDashboardRawModel>();
            while (sql.Read())
            {
                rawData.Add(new PersonalDashboardRawModel
                {
                    EmployeeId = sql.GetInt32(0),
                    EmployeeName = sql.GetString(1),
                    WorkingMonthly = sql.GetDecimal(2),
                    WorkingYearly = sql.GetDecimal(3),
                    SickMonthly = sql.GetInt32(4),
                    SickYearly = sql.GetInt32(5)
                });
            }
            return rawData;
        }

        private List<CompanyDashboardRawModel> CreateCompanyRawModel(DbDataReader sql)
        {
            List<CompanyDashboardRawModel> rawData = new List<CompanyDashboardRawModel>();
            while (sql.Read())
            {
                rawData.Add(new CompanyDashboardRawModel
                {
                    EmployeeId = sql.GetInt32(0),
                    TeamId = sql.GetInt32(1),
                    TeamName = sql.GetString(2),
                    RoleId = sql.GetInt32(3),
                    RoleName = sql.GetString(4),
                    RoleHourlyPrice = sql.GetDecimal(5),
                    RoleMonthlyPrice = sql.GetDecimal(6),
                    ProjectId = sql.GetInt32(7),
                    ProjectName = sql.GetString(8),
                    ProjectAmount = sql.GetDecimal(9),
                    ProjectPricingId = sql.GetInt32(10),
                    ProjectPricingName = sql.GetString(11),
                    WorkingHours = sql.GetDecimal(12)
                });
            }
            return rawData;
        }

        private List<AnnualRawModel> CreateAnnualRawModel(DbDataReader sql)
        {
            List<AnnualRawModel> rawData = new List<AnnualRawModel>();
            while (sql.Read())
            {
                rawData.Add(new AnnualRawModel
                {
                    Id = sql.GetInt32(0),
                    Name = sql.GetString(1),
                    Month = sql.GetInt32(2),
                    Hours = sql.GetDecimal(3)
                });
            }
            return rawData;            
        }

        private List<CompanyRolesRawModel> CreateCompanyRoles(DbDataReader sql)
        {
            List<CompanyRolesRawModel> rawData = new List<CompanyRolesRawModel>();
            while (sql.Read())
            {
                rawData.Add(new CompanyRolesRawModel
                {
                    EmployeeId = sql.GetInt32(0),
                    RoleId = sql.GetInt32(1),
                    RoleName = sql.GetString(2),
                    WorkingHours = sql.GetDecimal(3)
                });
            }
            return rawData;
        }

        private List<CompanyOvertimeModel> CreateCompanyOvertime(DbDataReader sql)
        {
            List<CompanyOvertimeModel> rawData = new List<CompanyOvertimeModel>();
            while (sql.Read())
            {
                rawData.Add(new CompanyOvertimeModel
                {
                    EmployeeId = sql.GetInt32(0),
                    EmployeeName = sql.GetString(1) + sql.GetString(2),
                    OvertimeHours = sql.GetDecimal(3)
                });
            }
            return rawData;
        }
               
        private List<CompanyEmployeeHoursModel> CreateEmployeeHours(DbDataReader sql)
        {
            List<CompanyEmployeeHoursModel> rawData = new List<CompanyEmployeeHoursModel>();
            while (sql.Read())
            {
                rawData.Add(new CompanyEmployeeHoursModel
                {
                    TeamId = sql.GetInt32(0),
                    EmployeeId = sql.GetInt32(1),
                    EmployeeName = sql.GetString(2) + " " + sql.GetString(3),
                    DayTypeId = sql.GetInt32(4),
                    DayTypeName = sql.GetString(5),
                    DayTypeHours = sql.GetDecimal(6)
                });
            }
            return rawData;
        }

    }
}
