using System.Threading.Tasks;
using CrossCutting.Persistance.SQL.Entities;

namespace CrossCutting.Persistance.SQL.Interfaces
{
    public interface IGenericUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;

        void Dispose();
        Task Save();
    }
}