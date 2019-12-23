using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        Task<IList<Entity>> GetAsync();
        Task<Entity> GetAsync(int id);
        Task<IList<Entity>> GetAsync(Expression<Func<Entity, bool>> where);
        Task InsertAsync(Entity entity);
        Task UpdateAsync(Entity entity, int id);
        void DeleteAsync(Entity entity);
        Task DeleteAsync(int id);
    }
}
