using Core.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Core.BaseDbContext
{
    public class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is IEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var entity = (IEntity)entry.Entity;
                var currentTime = DateTime.UtcNow;
                var currentUserId = 3; //GetCurrentUserId();
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = currentTime;
                    entity.CreatedBy = currentUserId;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entity.IsDeleted = true;
                }
                entity.ModifiedDate = currentTime;
                entity.ModifiedBy = currentUserId;
            }
        }
    }
}
