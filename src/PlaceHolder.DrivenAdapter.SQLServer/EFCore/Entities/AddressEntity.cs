using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;
using System;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities
{
    public class AddressEntity : TimeStampedEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }

        #region navigation properties

        public ConsumerEntity Consumer { get; set; }
        public Guid ConsumerId { get; set; }

        #endregion

        internal class AddressEntityConfiguration : TimeStampedEntityConfiguration<AddressEntity>
        {
            public override void Configure(EntityTypeBuilder<AddressEntity> builder)
            {
                base.Configure(builder);

                builder.ToTable("Address");

                builder.Property(a => a.Street)
                    .HasMaxLength(250);

                builder.Property(a => a.City)
                    .HasMaxLength(100);

                builder.Property(a => a.Country)
                    .HasMaxLength(100);

                builder.Property(a => a.PostalCode)
                    .HasMaxLength(6);
            }
        }
    }
}
