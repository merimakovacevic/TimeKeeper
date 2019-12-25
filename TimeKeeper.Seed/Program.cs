using OfficeOpenXml;
using System;
using System.IO;
using TimeKeeper.DAL;

namespace TimeKeeper.Seed
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FileInfo fileStatuses = new FileInfo(@"C:\Projects\TimeKeeper\TimeKeeper.Seed\LegacyDatabase\TimeKeeperStatuses.xlsx");
            FileInfo file = new FileInfo(@"C:\Projects\TimeKeeper\TimeKeeper.Seed\LegacyDatabase\TimeKeeper.xlsx");

            string conString = "User ID=postgres; Password=postgres; Server=localhost; Port=5432; Database=TimeKeeper; Integrated Security=true; Pooling=true;";

            UnitOfWork unit = new UnitOfWork(new TimeKeeperContext(conString));

            unit.SeedDatabase(file, fileStatuses);
        }
    }
}