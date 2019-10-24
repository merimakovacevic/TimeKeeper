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
            //int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Day d = new Day
                {
                    //Id = rawData.ReadInteger(row, 1),
                    //Employee = unit.Employees.Get(rawData.ReadInteger(row, 2)), 
                    Employee = unit.Employees.Get(Utility.employeesDictionary[rawData.ReadInteger(row, 2)]),
                    Date = rawData.ReadDateValue(row, 4),
                    DayType = unit.DayTypes.Get(Utility.dayTypesDictionary[rawData.ReadInteger(row, 3)])
                    //DayType = unit.DayTypes.Get(rawData.ReadInteger(row, 3))//
                };
                unit.Calendar.Insert(d);
                unit.Save();
                /*
                if(N % 100 == 0)
                {
                    unit.Save();
                }
                N++;
                */
                Utility.calendarDictionary.Add(oldId, d.Id);
            }
            //unit.Save(); //save after all instances have been inserted into the unit, in case ther are instances after N % 100
        }
    }
}
