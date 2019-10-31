using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class EmploymentStatuses
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);

                EmploymentStatus employmentStatus = new EmploymentStatus
                {
                    //Id = rawData.ReadInteger(row, 1) + 1,//Id will start from 1, since the original table has Id ordered, but starting from 0
                    Name = rawData.ReadString(row, 2)
                };

                unit.EmploymentStatuses.Insert(employmentStatus);
                unit.Save();

                Utility.employmentStatusesDictionary.Add(oldId, employmentStatus.Id);
            }
        }
    }
}

