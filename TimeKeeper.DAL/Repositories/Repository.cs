using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            old.Relate(entity);
        }

        public void Delete(Entity entity) => _dbSet.Remove(entity);

        public virtual void Delete(int id)
        {
            Entity old = Get(id);
            Delete(old);
        }
    }
}
