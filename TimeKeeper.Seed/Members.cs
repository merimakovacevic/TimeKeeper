using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Seed
{
    public class Members
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 6; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);
                Member member = new Member
                {
                    Employee = unit.Employees.Get(Utility.employeesDictionary[rawData.ReadInteger(row, 1)]),
                    Team = unit.Teams.Get(Utility.teamsDictionary[rawData.ReadString(row, 2)]),
                    Role = unit.Roles.Get(Utility.rolesDictionary[rawData.ReadString(row, 3)]),
                    HoursWeekly = rawData.ReadDecimal(row, 4)
                };
                unit.Members.Insert(member);
                unit.Save();
                Utility.membersDictionary.Add(oldId, member.Id);
            }
        }
    }
}
