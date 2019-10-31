using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class PricingStatuses
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);

                PricingStatus pricingStatus = new PricingStatus
                {
                    //Id = rawData.ReadInteger(row, 1) + 1,
                    Name = rawData.ReadString(row, 2)
                };

                unit.PricingStatuses.Insert(pricingStatus);
                unit.Save();

                Utility.pricingStatusesDictionary.Add(oldId, pricingStatus.Id);
            }
        }
    }
}

