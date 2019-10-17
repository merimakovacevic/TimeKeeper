using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class Calendar
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            //int s = rawData.ReadInteger(2, 2);
            //DateTime da = rawData.ReadDate1(3, 4);
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Day d = new Day
                {
                    Employee = unit.Employees.Get(Utility.employeesDictionary[rawData.ReadInteger(row, 2)]),
                    Date=rawData.ReadDate1(row, 4),
                    DayType=unit.DayTypes.Get(Utility.dayTypesDictionary[rawData.ReadInteger(row, 3)])
                };
                unit.Calendar.Insert(d);
                unit.Save();
                Utility.calendarDictionary.Add(oldId, d.Id);
            }
        }
    }
}
