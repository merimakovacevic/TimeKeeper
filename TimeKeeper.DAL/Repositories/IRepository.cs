using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeKeeper.DAL.Repositories
{
    public interface IRepository<Entity, K>
    {
        IQueryable<Entity> Get();
        Entity Get(K id);
        IList<Entity> Get(Func<Entity, bool> where);
        void Insert(Entity entity);
        void Update(Entity entity, K id);
        void Delete(Entity entity);
        void Delete(K id);
    }
}
