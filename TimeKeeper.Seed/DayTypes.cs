using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class DayTypes
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);

                DayType dayType = new DayType
                {
                    //Id = rawData.ReadInteger(row, 1),
                    Name = rawData.ReadString(row, 2)
                };

                unit.DayTypes.Insert(dayType);
                unit.Save();

                Utility.dayTypesDictionary.Add(oldId, dayType.Id);
            }
        }
    }
}
