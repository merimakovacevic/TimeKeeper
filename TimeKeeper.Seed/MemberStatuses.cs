using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class MemberStatuses
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                MemberStatus memberStatus = new MemberStatus
                {
                    //Id = rawData.ReadInteger(row, 1),
                    Name = rawData.ReadString(row, 2)
                };
                unit.MemberStatuses.Insert(memberStatus);
                unit.Save();                
            }
        }
    }
}
