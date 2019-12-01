using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeKeeper.DAL;
using TimeKeeper.Seed;

namespace TimeKeeper.Test
{
    [TestFixture]
    public class TestBaseTestDatabase
    {
        public TimeKeeperContext context;
        public UnitOfWork unit;

        [OneTimeSetUp]
        public void SetUp()
        {
            string conStr = "User ID=postgres; Password=postgres; Server=localhost; Port=5432; Database=TKTestera; Integrated Security=true; Pooling=true;";
            //Paths for test Excel databases
            FileInfo fileStatuses = new FileInfo(@"C:\Projects\TimeKeeper\TimeKeeper.Test\TestDatabase\TimeKeeperStatusesTest.xlsx");
            FileInfo file = new FileInfo(@"C:\Projects\TimeKeeper\TimeKeeper.Test\TestDatabase\TimeKeeperTest.xlsx");

            context = new TimeKeeperContext(conStr);
            unit = new UnitOfWork(context);
            unit.SeedDatabase(file, fileStatuses);
        }
    }
}
