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

            using (UnitOfWork unit = new UnitOfWork())
            {
                unit.Context.Database.EnsureDeleted();
                unit.Context.Database.EnsureCreated();

                using (ExcelPackage packageStatuses = new ExcelPackage(fileStatuses))
                {                 
                    EmployeePositions.Collect(packageStatuses.Workbook.Worksheets["EmployeePosition"], unit);
                    EmploymentStatuses.Collect(packageStatuses.Workbook.Worksheets["EmploymentStatus"], unit);
                    DayTypes.Collect(packageStatuses.Workbook.Worksheets["DayType"], unit);
                    CustomerStatuses.Collect(packageStatuses.Workbook.Worksheets["CustomerStatus"], unit);
                    ProjectStatuses.Collect(packageStatuses.Workbook.Worksheets["ProjectStatus"], unit);
                    PricingStatuses.Collect(packageStatuses.Workbook.Worksheets["PricingStatus"], unit);
                }
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    
                }
            }
        }
    }
}
