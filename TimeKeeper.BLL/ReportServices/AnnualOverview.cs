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
using TimeKeeper.DTO.ReportModels.AnnualOverview;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.BLL.ReportServices
{
    public class AnnualOverview : BLLBaseService
    {
        protected StoredProcedureService _storedProcedures;
        public AnnualOverview(UnitOfWork unit): base(unit)
        {
            _storedProcedures = new StoredProcedureService(unit);
        }
        public List<AnnualTimeModel> GetAnnualOverview(int year)
        {
            List<AnnualTimeModel> result = new List<AnnualTimeModel>();
            AnnualTimeModel total = new AnnualTimeModel { Project = new MasterModel { Id = 0, Name = "TOTAL" } };
            List<Project> projects = _unit.Projects.Get().OrderBy(p => p.Name).ToList();
            foreach (Project p in projects)
            {
                List<JobDetail> query = p.Tasks.Where(d => d.Day.Date.Year == year).ToList();
                if (query.Count != 0)
                {
                    var list = query.GroupBy(d => d.Day.Date.Month)
                                    .Select(x => new { month = x.Key, hours = x.Sum(y => y.Hours) });
                    AnnualTimeModel atm = new AnnualTimeModel { Project = p.Master() };
                    foreach (var item in list)
                    {
                        atm.Hours[item.month - 1] = item.hours;
                        atm.Total += item.hours;

                        total.Hours[item.month - 1] += item.hours;
                        total.Total += item.hours;
                    }
                    total.Project.Id++;
                    result.Add(atm);
                }
            }
            result.Add(total);
            return result;
        }

        public List<AnnualTimeModel> GetStored(int year)
        {
            List<AnnualTimeModel> result = new List<AnnualTimeModel>();
            AnnualTimeModel total = new AnnualTimeModel { Project = new MasterModel { Id = 0, Name = "TOTAL" } };
            List<AnnualRawModel> rawData = _storedProcedures.GetStoredProcedure<AnnualRawModel>("AnnualReport", new int[] { year });
            /*
            var cmd = _unit.Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"select * from AnnualReport({year})";
            if (cmd.Connection.State == ConnectionState.Closed) cmd.Connection.Open();
            DbDataReader sql = cmd.ExecuteReader();
            List<AnnualRawModel> rawData = new List<AnnualRawModel>();
            if (sql.HasRows)
            {
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
                */
                AnnualTimeModel atm = new AnnualTimeModel { Project = new MasterModel { Id = 0 } };
                foreach (AnnualRawModel item in rawData)
                {
                    if (atm.Project.Id != item.Id)
                    {
                        if (atm.Project.Id != 0) result.Add(atm);
                        atm = new AnnualTimeModel { Project = new MasterModel { Id = item.Id, Name = item.Name } };
                        total.Project.Id++;
                    }
                    atm.Hours[item.Month - 1] = item.Hours;
                    atm.Total += item.Hours;
                    total.Hours[item.Month - 1] += item.Hours;
                    total.Total += item.Hours;
                }
                if (atm.Project.Id != 0) result.Add(atm);
            //}
            result.Add(total);
            return result;
        }
    }
}
