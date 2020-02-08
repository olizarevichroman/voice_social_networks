using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoiceSocialNetworks.DataLayer.Abstractions;

namespace VoiceSocialNetworks.DataLayer.Implementations
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public DbContext Context { get; }
        public Repository(DbContext context)
        {
            Context = context;
        }
        public void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Add(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public Task<TEntity> GetEntity(object key)
        {
            return Context.Set<TEntity>().FindAsync(key).AsTask();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
    }
}
