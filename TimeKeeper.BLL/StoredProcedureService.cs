using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.DTO.ReportModels;
using TimeKeeper.DTO.ReportModels.AdminDashboard;
using TimeKeeper.DTO.ReportModels.AnnualOverview;

namespace TimeKeeper.BLL
{
    public class StoredProcedureService
    {
        protected UnitOfWork _unit;
        protected SQLFactory _sqlFactory;
        public StoredProcedureService(UnitOfWork unit)
        {
            _unit = unit;
            _sqlFactory = new SQLFactory();
        }
        public List<Entity> GetStoredProcedure<Entity>(string procedureName, int[] args)
        {
            var arguments = string.Join(", ", args);
            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = $"select * from {procedureName}({args})";
            cmd.CommandText = $"select * from {procedureName}({arguments})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            if (sql.HasRows)
            {
                //return _sqlFactory.BuildSQL<Entity>(sql);
                
                if (typeof(Entity) == typeof(AdminRawModel))
                {
                    List<AdminRawModel> rawData = new List<AdminRawModel>();
                    while (sql.Read()) rawData.Add(CreateAdminRawModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
                if (typeof(Entity) == typeof(AdminRawPTOModel))
                {
                    List<AdminRawPTOModel> rawData = new List<AdminRawPTOModel>();
                    while (sql.Read()) rawData.Add(CreateAdminRawPTOModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }                
                if (typeof(Entity) == typeof(AnnualRawModel))
                {
                    List<AnnualRawModel> rawData = new List<AnnualRawModel>();
                    while (sql.Read()) rawData.Add(CreateAnnualRawModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
                if (typeof(Entity) == typeof(RawMasterModel))
                {
                    List<RawMasterModel> rawData = new List<RawMasterModel>();
                    while (sql.Read()) rawData.Add(CreateRawMasterModel(sql));
                    cmd.Connection.Close();
                    return rawData as List<Entity>;
                }
            }

            return null;
        }
        
        public AdminRawModel CreateAdminRawModel(DbDataReader sql)
        {

            return new AdminRawModel
            {
                EmployeeId = sql.GetInt32(0),
                RoleId = sql.GetInt32(1),
                RoleName = sql.GetString(2),
                TeamId = sql.GetInt32(3),
                ProjectId = sql.GetInt32(4),
                WorkingHours = sql.GetDecimal(5)
            };
        }

        public AdminRawPTOModel CreateAdminRawPTOModel(DbDataReader sql)
        {

            return new AdminRawPTOModel
            {
                TeamId = sql.GetInt32(0),
                Team = sql.GetString(1),
                PaidTimeOff = sql.GetDecimal(2)
            };
        }

        public AnnualRawModel CreateAnnualRawModel(DbDataReader sql)
        {
            return new AnnualRawModel
            {
                Id = sql.GetInt32(0),
                Name = sql.GetString(1),
                Month = sql.GetInt32(2),
                Hours = sql.GetDecimal(3)
            };
        }

        public RawMasterModel CreateRawMasterModel(DbDataReader sql)
        {
            return new RawMasterModel
            {
                Id = sql.GetInt32(0),
                Name = sql.GetString(1),
                Value = sql.GetDecimal(2)
            };
        }
    }
}
