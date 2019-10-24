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
                    //Id = rawData.ReadInteger(row, 1),
                    Name = rawData.ReadString(row, 3),
                    Description = rawData.ReadString(row, 4),
                    StartDate = rawData.ReadDate(row, 5),
                    EndDate = rawData.ReadDate(row, 6),
                    Status = unit.ProjectStatuses.Get(Utility.projectStatusesDictionary[rawData.ReadInteger(row, 7)]),
                    //Status = unit.ProjectStatuses.Get(rawData.ReadInteger(row, 7)),
                    Customer = unit.Customers.Get(Utility.customersDictionary[rawData.ReadInteger(row, 8)]),
                    //Customer = unit.Customers.Get(rawData.ReadInteger(row, 8)), 
                    Team = unit.Teams.Get(Utility.teamsDictionary[rawData.ReadString(row, 9)]),
                    Pricing = unit.PricingStatuses.Get(Utility.pricingStatusesDictionary[rawData.ReadInteger(row, 10)]),
                    //Pricing = unit.PricingStatuses.Get(rawData.ReadInteger(row, 10) + 1),//
                    Amount = rawData.ReadDecimal(row, 11)
                };

                unit.Projects.Insert(p);
                unit.Save();

                Utility.projectsDictionary.Add(oldId, p.Id);
                Console.WriteLine("PROJECTS: Old id: " + oldId + " new id: " + p.Id);
            }
            //unit.Save();
        }
    }
}
