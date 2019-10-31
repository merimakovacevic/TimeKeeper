using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeKeeper.DAL;

namespace TimeKeeper.Seed
{
    public static class SeedData
    {
        public static void SeedDatabase(this UnitOfWork unit, FileInfo fileCoreData, FileInfo fileStatusesData)
        {
            unit.Context.Database.EnsureDeleted();
            unit.Context.Database.EnsureCreated();
            unit.Context.ChangeTracker.AutoDetectChangesEnabled = false;

            using (ExcelPackage packageStatuses = new ExcelPackage(fileStatusesData))
            {
                EmployeePositions.Collect(packageStatuses.Workbook.Worksheets["EmployeePosition"], unit);
                EmploymentStatuses.Collect(packageStatuses.Workbook.Worksheets["EmploymentStatus"], unit);
                DayTypes.Collect(packageStatuses.Workbook.Worksheets["DayType"], unit);
                CustomerStatuses.Collect(packageStatuses.Workbook.Worksheets["CustomerStatus"], unit);
                ProjectStatuses.Collect(packageStatuses.Workbook.Worksheets["ProjectStatus"], unit);
                PricingStatuses.Collect(packageStatuses.Workbook.Worksheets["PricingStatus"], unit);
                MemberStatuses.Collect(packageStatuses.Workbook.Worksheets["MemberStatus"], unit);
            }
            using (ExcelPackage package = new ExcelPackage(fileCoreData))
            {
                Teams.Collect(package.Workbook.Worksheets["Teams"], unit);
                Roles.Collect(package.Workbook.Worksheets["Roles"], unit);
                Customers.Collect(package.Workbook.Worksheets["Customers"], unit);
                Projects.Collect(package.Workbook.Worksheets["Projects"], unit);
                Employees.Collect(package.Workbook.Worksheets["Employees"], unit);
                Calendar.Collect(package.Workbook.Worksheets["Calendar"], unit);
                Members.Collect(package.Workbook.Worksheets["Engagement"], unit);
                Details.Collect(package.Workbook.Worksheets["Details"], unit);
            }

            Console.WriteLine("Seed complete!");
        }
    }
}
