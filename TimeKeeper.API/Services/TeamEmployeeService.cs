using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.DAL;

namespace TimeKeeper.API.Services
{
    public class TeamEmployeeService
    {
        private UnitOfWork _unit { get; set; }
        public TeamEmployeeService(TimeKeeperContext context)
        {
            _unit = new UnitOfWork(context);
        }
        /*
        public IsLeadToAnyTeam(int empId, string team)
        {

        }
        */
    }
}
