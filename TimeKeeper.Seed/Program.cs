using OfficeOpenXml;
using System;
using System.IO;
using TimeKeeper.DAL;

namespace TimeKeeper.Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo fileStatuses = new FileInfo(@"C:\TimeKeeper\TimeKeeperStatuses.xlsx");
            FileInfo file = new FileInfo(@"C:\TimeKeeper\TimeKeeper.xlsx");
            using (UnitOfWork unit = new UnitOfWork())
            {
                using (ExcelPackage packageStatuses = new ExcelPackage(fileStatuses))
                {
                    unit.Context.Database.EnsureDeleted();
                    unit.Context.Database.EnsureCreated();
                    EmployeePositions.Collect(packageStatuses.Workbook.Worksheets["EmployeePosition"], unit);
                    EmploymentStatuses.Collect(packageStatuses.Workbook.Worksheets["EmploymentStatus"], unit);
                    DayTypes.Collect(packageStatuses.Workbook.Worksheets["DayType"], unit);
                    CustomerStatuses.Collect(packageStatuses.Workbook.Worksheets["CustomerStatus"], unit);
                    ProjectStatuses.Collect(packageStatuses.Workbook.Worksheets["ProjectStatus"], unit);
                    PricingStatuses.Collect(packageStatuses.Workbook.Worksheets["PricingStatus"], unit);


                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    Teams.Collect(package.Workbook.Worksheets["Teams"], unit);
                    Roles.Collect(package.Workbook.Worksheets["Roles"], unit);
                    Customers.Collect(package.Workbook.Worksheets["Customers"], unit);
                    Projects.Collect(package.Workbook.Worksheets["Projects"], unit);
                    Employees.Collect(package.Workbook.Worksheets["Employees"], unit);
                    Calendar.Collect(package.Workbook.Worksheets["Calendar"], unit);
                }
            }
        }
    }
}
