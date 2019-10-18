using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeKeeper.DAL.Repositories
{
    public interface IRepository<Entity>
    {
        IQueryable<Entity> Get();
        Entity Get(int id);
        IList<Entity> Get(Func<Entity, bool> where);
        void Insert(Entity entity);
        void Update(Entity entity, int id);
        void Delete(Entity entity);
        void Delete(int id);
    }
}
