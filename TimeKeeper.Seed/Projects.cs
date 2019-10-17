using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class Projects
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Project p = new Project
                {
                    Name = rawData.ReadString(row, 3),
                    Description = rawData.ReadString(row, 4),
                    Team = unit.Teams.Get(Utility.teamsDictionary[rawData.ReadString(row, 9)]),
                    Customer = unit.Customers.Get(Utility.customersDictionary[rawData.ReadInteger(row, 8)]),
                    StartDate = rawData.ReadDate(row, 5),
                    EndDate = rawData.ReadDate(row, 6),
                    Status = unit.ProjectStatuses.Get(rawData.ReadInteger(row, 7)),
                    Pricing = unit.PricingStatuses.Get(Utility.pricingStatusesDictionary[rawData.ReadInteger(row, 10)]),
                    Amount = rawData.ReadDecimal(row, 11)
                };
                unit.Projects.Insert(p);
                unit.Save();
                Utility.projectsDictionary.Add(oldId, p.Id);
            }
        }
    }
}
