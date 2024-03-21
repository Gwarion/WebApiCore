using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities
{
    public abstract class TimeStampedEntity
    {
        public Guid Guid { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public class TimeStampedEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
            where TEntity : TimeStampedEntity
        {
            public virtual void Configure(EntityTypeBuilder<TEntity> builder)
            {
                builder.Property(t => t.Guid)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<GuidValueGenerator>();

                builder.HasKey(e => e.Guid);
            }
        }
    }
}
