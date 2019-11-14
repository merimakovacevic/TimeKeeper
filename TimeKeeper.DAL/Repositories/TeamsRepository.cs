using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class TeamsRepository: Repository<Team>
    {
        public TeamsRepository(TimeKeeperContext context) : base(context) { }

        public override void Delete(int id)
        {
            Team old = Get(id);

            if (old.Projects.Count != 0 || old.Members.Count != 0)
            {
                Services.ThrowChildrenPresentException();
            }

            Delete(old); 
        }
    }
}
