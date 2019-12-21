using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class RolesRepository : Repository<Role>
    {
        public RolesRepository(TimeKeeperContext context) : base(context) { }

        public override void Delete(int id)
        {
            Role old = Get(id);

            if (!old.CanDelete())
            {
                Services.ThrowChildrenPresentException();
            }

            Delete(old);
        }
    }
}
