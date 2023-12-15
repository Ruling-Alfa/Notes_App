using CrossCutting.Persistance.SQL;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Data
{
    public class ApplicationUnitOfWork : GenericUnitOfWork , IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(DbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
        }
    }
}
