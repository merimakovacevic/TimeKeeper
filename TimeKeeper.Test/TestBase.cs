using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;

namespace TimeKeeper.Test
{
    public class TestBase
    {
        public TimeKeeperContext context;
        public UnitOfWork unit;

        [OneTimeSetUp]
        public void SetUp()
        {
            string conStr = "User ID=postgres; Password=postgres; Server=localhost; Port=5432; Database=TKTestera; Integrated Security=true; Pooling=true;";

            context = new TimeKeeperContext(conStr);
            unit = new UnitOfWork(context);
            unit.Seed();
        }
    }
}
