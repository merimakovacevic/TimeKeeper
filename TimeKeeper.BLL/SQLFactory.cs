using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using TimeKeeper.DTO.ReportModels.AdminDashboard;

namespace TimeKeeper.BLL
{
    public class SQLFactory
    {
        public List<Entity> BuildSQL<Entity>(DbDataReader sql)
        {
            if (typeof(Entity) == typeof(AdminRawModel)) return CreateAdminRawModel(sql) as List<Entity>;
            if (typeof(Entity) == typeof(AdminRawPTOModel)) return CreateAdminRawPTOModel(sql) as List<Entity>;

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
                    ProjectId = sql.GetInt32(1),
                    WorkingHours = sql.GetDecimal(2)
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
    }
}
