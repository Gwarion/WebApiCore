using Microsoft.EntityFrameworkCore;
using PlaceHolder.Application.Services.Ports.EF;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts
{
    public class PlaceHolderContext : DbContext, IDbContext
    {
        public DbSet<ConsumerEntity> Consumers { get; set; }
        public PlaceHolderContext(DbContextOptions options) : base(options) { }

        public async Task<T> ExecuteAsTransaction<T>(Func<Task<T>> action) => await action();
        public void Migrate() { return; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;

            ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is TimeStampedEntity)
                .Select(e => e.Entity as TimeStampedEntity).ToList()
                .ForEach(e => e.CreationDate = e.ModificationDate = now);

            ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is TimeStampedEntity)
                .Select(e => e.Entity as TimeStampedEntity).ToList()
                .ForEach(e => e.ModificationDate = now);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
