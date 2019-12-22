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
            if (typeof(Entity) == typeof(AdminEmployeeHoursModel)) return CreateEmployeeHoursModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AdminOvertimeModel)) return CreateAdminOvertimeModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(EmployeeHoursRawModel)) return CreateEmployeeHoursRawModel(sql) as List<Entity>;

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

        public List<AdminOvertimeModel> CreateAdminOvertimeModel(DbDataReader sql)
        {
            List<AdminOvertimeModel> rawData = new List<AdminOvertimeModel>();
            while (sql.Read())
            {
                rawData.Add(new AdminOvertimeModel
                {
                    EmployeeId = sql.GetInt32(0),
                    EmployeeName = sql.GetString(1) + sql.GetString(2),
                    OvertimeHours = sql.GetDecimal(3)
                });
            }
            return rawData;
        }

        public List<AdminEmployeeHoursModel> CreateEmployeeHoursModel(DbDataReader sql)
        {
            List<AdminEmployeeHoursModel> rawData = new List<AdminEmployeeHoursModel>();
            while (sql.Read())
            {
                rawData.Add(new AdminEmployeeHoursModel
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

        public List<EmployeeHoursRawModel> CreateEmployeeHoursRawModel(DbDataReader sql)
        {
            List<EmployeeHoursRawModel> rawData = new List<EmployeeHoursRawModel>();
            while (sql.Read())
            {
                rawData.Add(new EmployeeHoursRawModel
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
