using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL;

namespace TimeKeeper.BLL
{
    public class BLLBaseService
    {
        protected UnitOfWork _unit;
        public BLLBaseService(UnitOfWork unit)
        {
            _unit = unit;
        }
    }
}
