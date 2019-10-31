using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class CustomerStatuses
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);

                CustomerStatus customerStatus = new CustomerStatus
                {
                    //Id = rawData.ReadInteger(row, 1) + 1,
                    Name = rawData.ReadString(row, 2)
                };

                unit.CustomerStatuses.Insert(customerStatus);
                unit.Save();
                Utility.customerStatusesDictionary.Add(oldId, customerStatus.Id);
            }
        }
    }
}
