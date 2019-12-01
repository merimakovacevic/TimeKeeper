using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;

namespace TimeKeeper.Test
{
    [TestFixture]
    public class TestBaseRealDatabase
    {
        public TimeKeeperContext context;
        public UnitOfWork unit;

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new TimeKeeperContext();
            unit = new UnitOfWork(context);
        }
    }
}
