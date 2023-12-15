using CrossCutting.Persistance.SQL.Entities;
using CRUD_API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString(nameof(ApplicationContext)));
        }

        public override int SaveChanges()
        {
            UpdateTrackingProperties();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateTrackingProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateTrackingProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void UpdateTrackingProperties()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                var currentTime = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = currentTime;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = currentTime;
                }
            }
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
    }
}
