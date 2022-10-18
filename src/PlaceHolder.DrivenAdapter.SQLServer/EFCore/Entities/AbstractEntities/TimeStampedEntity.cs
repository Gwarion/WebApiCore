using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities
{
    public abstract class TimeStampedEntity
    {
        [Key]
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
            }
        }
    }
}
