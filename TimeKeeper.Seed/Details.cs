using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class Details
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 4);
                JobDetail j = new JobDetail
                {
                    Day = unit.Calendar.Get(Utility.calendarDictionary[rawData.ReadInteger(row, 1)]),
                    Position = unit.EmployeePositions.Get(Utility.employeePositionsDictionary[rawData.ReadString(row, 12)])
                };
                unit.Employees.Insert(j);
                unit.Save();
                Utility..Add(oldId, e.Id);
            }
        }
    }
}
