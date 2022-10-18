using Microsoft.EntityFrameworkCore;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts
{
    public class PlaceHolderContext : DbContext
    {
        public DbSet<Consumer> Consumers { get; set; }
        public PlaceHolderContext(DbContextOptions options) : base(options) { }

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

    }
}
