using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrossCutting.Persistance.SQL.Entities;

namespace CrossCutting.Persistance.SQL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task Delete(object id);
        void Delete(TEntity entityToDelete);
        Task<TEntity> GetOneByQuery(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        ValueTask<TEntity> GetByID(object id);
        ValueTask<EntityEntry<TEntity>> Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        Task<bool> ExistsById(int id);
    }
}
