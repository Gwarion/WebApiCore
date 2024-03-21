using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities
{
    public abstract class TimeStampedEntity
    {
        [Key]
        public Guid Guid { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public class TimeStampedEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
            where TEntity : TimeStampedEntity
        {
            public virtual void Configure(EntityTypeBuilder<TEntity> builder)
            {
                builder.Property(entity => entity.Guid)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<GuidValueGenerator>();

                builder.HasKey(entity => entity.Guid);
            }
        }
    }
}
