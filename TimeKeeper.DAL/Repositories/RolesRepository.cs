using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class RolesRepository : Repository<Role>
    {
        public RolesRepository(TimeKeeperContext context) : base(context) { }

        public override void Delete(int id)
        {
            Role old = Get(id);

            if (old.Members.Count != 0)
                throw new Exception("Object cannot be deleted because child objects are present");

            Delete(old);
        }
    }
}
