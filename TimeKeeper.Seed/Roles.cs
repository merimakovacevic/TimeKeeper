using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class Roles
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                string oldId = rawData.ReadString(row, 1);

                Role r = new Role
                {
                    Name = rawData.ReadString(row, 2),
                    HourlyPrice = rawData.ReadDecimal(row,3),
                    MonthlyPrice = rawData.ReadDecimal(row, 4)
                };

                unit.Roles.Insert(r);
                unit.Save();

                Utility.rolesDictionary.Add(oldId, r.Id);
            }
        }
    }
}
