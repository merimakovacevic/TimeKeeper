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
            FileInfo fileStatuses = new FileInfo(@"C:\Projects\TimeKeeper\TimeKeeperStatuses.xlsx");
            FileInfo file = new FileInfo(@"C:\Projects\TimeKeeper\TimeKeeper.xlsx");
            
            string conString = "User ID=postgres; Password=postgres; Server=localhost; Port=5432; Database=TimeKeeper; Integrated Security=true; Pooling=true;";

            using (UnitOfWork unit = new UnitOfWork(new TimeKeeperContext(conString)))
            {
                using (ExcelPackage packageStatuses = new ExcelPackage(fileStatuses))
                {
                    unit.Context.Database.EnsureDeleted();
                    unit.Context.Database.EnsureCreated();
                    EmployeePositions.Collect(packageStatuses.Workbook.Worksheets["EmployeePosition"], unit);//refactoring complete
                    EmploymentStatuses.Collect(packageStatuses.Workbook.Worksheets["EmploymentStatus"], unit);//refactoring complete
                    DayTypes.Collect(packageStatuses.Workbook.Worksheets["DayType"], unit);//refactoring complete
                    CustomerStatuses.Collect(packageStatuses.Workbook.Worksheets["CustomerStatus"], unit);//refactoring complete
                    ProjectStatuses.Collect(packageStatuses.Workbook.Worksheets["ProjectStatus"], unit);//refactoring complete
                    PricingStatuses.Collect(packageStatuses.Workbook.Worksheets["PricingStatus"], unit);//refactoring complete
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    Teams.Collect(package.Workbook.Worksheets["Teams"], unit);//refactoring complete
                    Roles.Collect(package.Workbook.Worksheets["Roles"], unit);//refactoring complete
                    Customers.Collect(package.Workbook.Worksheets["Customers"], unit);//refactoring complete
                    Projects.Collect(package.Workbook.Worksheets["Projects"], unit);//refactoring complete
                    Employees.Collect(package.Workbook.Worksheets["Employees"], unit);//refactoring complete
                    Calendar.Collect(package.Workbook.Worksheets["Calendar"], unit);//refactoring complete
                    Members.Collect(package.Workbook.Worksheets["Engagement"], unit);//refactoring complete
                    Details.Collect(package.Workbook.Worksheets["Details"], unit);
                }
            }
            
        }
    }
}
