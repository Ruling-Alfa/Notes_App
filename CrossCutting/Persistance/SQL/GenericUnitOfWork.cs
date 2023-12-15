using CrossCutting.Persistance.SQL.Entities;
using CrossCutting.Persistance.SQL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;


namespace CrossCutting.Persistance.SQL
{
    public class GenericUnitOfWork : IDisposable, IGenericUnitOfWork { 
        private readonly DbContext _dbContext;
        private readonly IServiceProvider _services;
        //private IGenericRepository<TEntity> _repo;
        public GenericUnitOfWork(DbContext dbContext, IServiceProvider services)
        {
            _dbContext = dbContext;
            _services = services;
            //_repo = repo;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
                return _services.GetService<IGenericRepository<TEntity>>();
        }

        public Task Save()
        {
            return _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
