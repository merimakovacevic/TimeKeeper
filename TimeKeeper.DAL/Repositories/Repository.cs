using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class Repository<Entity>: IRepository<Entity> where Entity : class
    {
        protected TimeKeeperContext _context;
        protected DbSet<Entity> _dbSet;

        public Repository(TimeKeeperContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public void ValidateUpdate(Entity newEntity, int id)
        {
            if (id != (newEntity as BaseClass).Id)
                throw new ArgumentException($"Error! Id of the sent object: {(newEntity as BaseClass).Id} and id in url: {id} do not match");
        }

        public virtual IQueryable<Entity> Get() => _dbSet;//adjust to Sulejman's code?

        public virtual IList<Entity> Get(Func<Entity, bool> where) => Get().Where(where).ToList();

        //public virtual Entity Get(int id) => _dbSet.Find(id);
        public virtual Entity Get(int id)
        {
            Entity entity = _dbSet.Find(id);
            if (entity == null)
                throw new ArgumentException($"There is no object with id: {id} in the database");
            return entity;
        }

        public virtual void Insert(Entity entity)
        {
            entity.Build(_context);
            _dbSet.Add(entity);
        }

        public virtual void Update(Entity entity, int id)
        {
            entity.Build(_context);
            Entity old = Get(id);
            ValidateUpdate(entity, id);
            _context.Entry(old).CurrentValues.SetValues(entity);
            old.Update(entity);
        }

        public void Delete(Entity entity) => _dbSet.Remove(entity);

        public virtual void Delete(int id)
        {
            Entity old = Get(id);
            Delete(old);
        }

        public async Task<IList<Entity>> GetAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Entity> GetAsync(int id)
        {
            Entity entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"There is no object with id: {id} in the database");
            }
            return entity;
        }

        public async Task<IList<Entity>> GetAsync(Expression<Func<Entity, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public async Task InsertAsync(Entity newEnt)
        {
            await newEnt.Build(_context);
            newEnt.Validate();
            _dbSet.Add(newEnt);
        }

        public async Task UpdateAsync(Entity newEnt, int id)
        {
            Entity oldEnt = await GetAsync(id);
            ValidateUpdate(newEnt, id);
            if (oldEnt != null)
            {
                await newEnt.Build(_context);
                if (typeof(Entity) == typeof(User)) (newEnt as User).Password = (oldEnt as User).Password;
                _context.Entry(oldEnt).CurrentValues.SetValues(newEnt);
                oldEnt.Update(newEnt);
            }
        }

        public void DeleteAsync(Entity entity) => _dbSet.Remove(entity);

        public async Task DeleteAsync(int id)
        {
            Entity entity = await GetAsync(id);
            if (entity != null && entity.CanDelete()) Delete(entity);
        }
    }
}
