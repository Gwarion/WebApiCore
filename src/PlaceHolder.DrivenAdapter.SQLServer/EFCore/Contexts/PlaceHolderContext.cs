using Microsoft.EntityFrameworkCore;
using PlaceHolder.Application.Services.Ports.EF;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts
{
    public class PlaceHolderContext : DbContext, IDbContext
    {
        public DbSet<ConsumerEntity> Consumers { get; set; }
        public PlaceHolderContext(DbContextOptions options) : base(options) { }

        public void Migrate() => this.Database.Migrate();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlaceHolderContext).Assembly);
        }

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

        public async Task<T> ExecuteAsTransaction<T>(Func<Task<T>> transactionalAction)
        {
            try
            {
                using var transaction = await this.Database.BeginTransactionAsync();
                var result = await transactionalAction();
                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                if(this.Database?.CurrentTransaction?.TransactionId != null)
                {
                    this.Database.RollbackTransaction();
                }

                throw;
            }   
        }
    }
}
