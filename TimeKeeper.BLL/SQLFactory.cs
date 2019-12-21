using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using TimeKeeper.DTO.ReportModels.AdminDashboard;
using TimeKeeper.DTO.ReportModels.AnnualOverview;

namespace TimeKeeper.BLL
{
    public class SQLFactory
    {
        public List<Entity> BuildSQL<Entity>(DbDataReader sql)
        {
            if (typeof(Entity) == typeof(AdminRawModel)) return CreateAdminRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AdminRawPTOModel)) return CreateAdminRawPTOModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AnnualRawModel)) return CreateAnnualRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AdminRolesRawModel)) return CreateAdminRolesRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AdminEmployeeHoursModel)) return CreateAdminEmployeeHoursModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AdminOvertimeModel)) return CreateAdminOvertimeModel(sql) as List<Entity>;

            else return null;
        }        

        public List<AdminRawModel> CreateAdminRawModel(DbDataReader sql)
        {
            List<AdminRawModel> rawData = new List<AdminRawModel>();
            while (sql.Read())
            {
                rawData.Add(new AdminRawModel
                {
                    EmployeeId = sql.GetInt32(0),
                    RoleId = sql.GetInt32(1),
                    RoleName = sql.GetString(2),
                    RoleHourlyPrice = sql.GetDecimal(3),
                    RoleMonthlyPrice = sql.GetDecimal(4),
                    ProjectId = sql.GetInt32(5),
                    ProjectName = sql.GetString(6),
                    ProjectAmount = sql.GetDecimal(7),
                    ProjectPricingId = sql.GetInt32(8),
                    ProjectPricingName = sql.GetString(9),
                    WorkingHours = sql.GetDecimal(10)
                });
            }
            return rawData;
        }

        public List<AdminRawPTOModel> CreateAdminRawPTOModel(DbDataReader sql)
        {
            List<AdminRawPTOModel> rawData = new List<AdminRawPTOModel>();
            while (sql.Read())
            {
                rawData.Add(new AdminRawPTOModel
                {
                    TeamId = sql.GetInt32(0),
                    Team = sql.GetString(1),
                    PaidTimeOff = sql.GetDecimal(2)
                });
            }
            return rawData;
        }

        public List<AnnualRawModel> CreateAnnualRawModel(DbDataReader sql)
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

        public List<AdminRolesRawModel> CreateAdminRolesRawModel(DbDataReader sql)
        {
            List<AdminRolesRawModel> rawData = new List<AdminRolesRawModel>();
            while (sql.Read())
            {
                rawData.Add(new AdminRolesRawModel
                {
                    EmployeeId = sql.GetInt32(0),
                    RoleId = sql.GetInt32(1),
                    RoleName = sql.GetString(2),
                    WorkingHours = sql.GetDecimal(3)
                });
            }
            return rawData;
        }

        public List<AdminEmployeeHoursModel> CreateAdminEmployeeHoursModel(DbDataReader sql)
        {
            List<AdminEmployeeHoursModel> rawData = new List<AdminEmployeeHoursModel>();
            while (sql.Read())
            {
                rawData.Add(new AdminEmployeeHoursModel
                {
                    EmployeeId = sql.GetInt32(0),
                    SumOfHours = sql.GetDecimal(1)
                });
            }
            return rawData;
        }
        public List<AdminOvertimeModel> CreateAdminOvertimeModel(DbDataReader sql)
        {
            List<AdminOvertimeModel> rawData = new List<AdminOvertimeModel>();
            while (sql.Read())
            {
                rawData.Add(new AdminOvertimeModel
                {
                    TeamId = sql.GetInt32(0),
                    TeamName = sql.GetString(1),
                    OvertimeHours = sql.GetDecimal(2)
                });
            }
            return rawData;
        }
    }
}
