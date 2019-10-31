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
            //int N = 0;
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                JobDetail j = new JobDetail
                {
                    Description = rawData.ReadString(row, 1),
                    Hours = rawData.ReadDecimal(row, 2),
                    //Day = unit.Calendar.Get(rawData.ReadInteger(row, 4)),
                    Day = unit.Calendar.Get(Utility.calendarDictionary[rawData.ReadInteger(row, 4)]),
                    //Project = unit.Projects.Get(rawData.ReadInteger(row, 3))
                    Project = unit.Projects.Get(Utility.projectsDictionary[rawData.ReadInteger(row, 3)])
                };

                unit.Tasks.Insert(j);            

                /*
                if (N % 100 == 0)
                {
                    unit.Save();
                }
                N++;*/
            }
            unit.Save();            
        }
    }
}
