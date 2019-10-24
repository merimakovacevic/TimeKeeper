using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class Customers
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Customer c = new Customer
                {
                    //Id = rawData.ReadInteger(row, 1),
                    Name = rawData.ReadString(row, 2),
                    Image = rawData.ReadString(row, 3),
                    ContactName = rawData.ReadString(row, 4),
                    EmailAddress = rawData.ReadString(row, 5),
                    Status = unit.CustomerStatuses.Get(Utility.customerStatusesDictionary[rawData.ReadInteger(row, 10)]),
                    //Status = unit.CustomerStatuses.Get(rawData.ReadInteger(row, 10) + 1),  //unit.CustomerStatuses.Get(Utility.customerStatusesDictionary[rawData.ReadInteger(row, 10)]),
                    HomeAddress = new Address()
                };

                c.HomeAddress.Street = rawData.ReadString(row, 7);
                c.HomeAddress.Zip = rawData.ReadString(row, 8);
                c.HomeAddress.City = rawData.SelectCity(row, 9);
                c.HomeAddress.Country = rawData.SelectCountry(row, 9);

                unit.Customers.Insert(c);
                unit.Save();
                Utility.customersDictionary.Add(oldId, c.Id);
            }
            //unit.Save();
        }
    }
}
