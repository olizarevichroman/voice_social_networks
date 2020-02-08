using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VoiceSocialNetworks.DataLayer.Abstractions
{
    public interface IRepository<TEntity> where TEntity: class, new()
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetEntity(object key);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
