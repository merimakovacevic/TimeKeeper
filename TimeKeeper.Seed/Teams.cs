using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class Teams
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                string oldId = rawData.ReadString(row, 1);
                Team t = new Team
                {
                    Name= rawData.ReadString(row, 2)
                };
                unit.Teams.Insert(t);
                unit.Save();
                Utility.teamsDictionary.Add(oldId, t.Id);
            }
        }
    }
}
