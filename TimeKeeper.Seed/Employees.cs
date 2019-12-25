using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;
using TimeKeeper.Utility.Factory;

namespace TimeKeeper.Seed
{
    public class Employees
    {
        public static void Collect(ExcelWorksheet rawData, UnitOfWork unit)
        {
            for (int row = 2; row <= rawData.Dimension.Rows; row++)
            {
                int oldId = rawData.ReadInteger(row, 1);

                Employee employee = new Employee
                {
                    //Id = rawData.ReadInteger(row, 1),
                    FirstName = rawData.ReadString(row, 2),
                    LastName = rawData.ReadString(row, 3),
                    Image = rawData.ReadString(row, 4),
                    Email = rawData.ReadString(row, 6),
                    Phone = rawData.ReadString(row, 7),
                    Birthday = rawData.ReadDate(row, 8),
                    BeginDate = rawData.ReadDate(row, 9),
                    EndDate = rawData.ReadDate(row, 10),
                    //status is + 1, because the Id in the status table has only been incremented for 1 value compared to the legacy database
                    Status = unit.EmploymentStatuses.Get(Utility.employmentStatusesDictionary[rawData.ReadInteger(row, 11)]),
                    //Status = unit.EmploymentStatuses.Get(rawData.ReadInteger(row, 11) + 1), 
                    Position = unit.EmployeePositions.Get(Utility.employeePositionsDictionary[rawData.ReadString(row, 12)]),
                    Salary = rawData.ReadDecimal(row, 13)//additionally added to entity properties
                };

                unit.Employees.Insert(employee);
                unit.Save();

                //User insertion is binded to employee insertion
                User user = employee.CreateUserAndRole();

                unit.Users.Insert(user);
                unit.Save();

                Utility.employeesDictionary.Add(oldId, employee.Id);
                Console.WriteLine("EMPLOYEES: Old id: " + oldId + "new id: " + employee.Id);
            }
            //unit.Save();
        }
    }
}
