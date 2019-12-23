using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using TimeKeeper.DTO.ReportModels.AnnualOverview;
using TimeKeeper.DTO.ReportModels.CompanyDashboard;

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

            else return null;
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
