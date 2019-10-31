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
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                Member member = new Member
                {
                    //Employee = unit.Employees.Get(rawData.ReadInteger(row, 1)),
                    Employee = unit.Employees.Get(Utility.employeesDictionary[rawData.ReadInteger(row, 1)]),
                    Team = unit.Teams.Get(Utility.teamsDictionary[rawData.ReadString(row, 2)]),
                    Role = unit.Roles.Get(Utility.rolesDictionary[rawData.ReadString(row, 3)]),
                    Status = unit.MemberStatuses.Get(2),//since the legacy database doesn't have data on member's status, all values will be set to status Id 2 (Active) by default 
                    HoursWeekly = rawData.ReadDecimal(row, 4)
                };

                unit.Members.Insert(member);
                Console.WriteLine("MEMBERS: Employee id: " + member.Employee.Id);
            }
            unit.Save();
        }
    }
}
