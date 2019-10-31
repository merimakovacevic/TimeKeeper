using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class EmployeePositions
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                string oldId = rawData.ReadString(row, 1);

                EmployeePosition employeePosition = new EmployeePosition
                {
                    Name = rawData.ReadString(row, 2)
                };

                unit.EmployeePositions.Insert(employeePosition);
                unit.Save();

                Utility.employeePositionsDictionary.Add(oldId, employeePosition.Id);
            }
        }
    }
}

