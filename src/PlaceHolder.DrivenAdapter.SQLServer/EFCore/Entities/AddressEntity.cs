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

        /// <summary>
        /// Consumer foreign key
        /// </summary>
        public Guid ConsumerId { get; set; }

        internal class AddressEntityConfiguration : TimeStampedEntityConfiguration<AddressEntity>
        {
            public override void Configure(EntityTypeBuilder<AddressEntity> builder)
            {
                base.Configure(builder);

                builder.ToTable("Address");

                builder.Property(address => address.Street)
                    .HasMaxLength(250);

                builder.Property(address => address.City)
                    .HasMaxLength(100);

                builder.Property(address => address.Country)
                    .HasMaxLength(100);

                builder.Property(address => address.PostalCode)
                    .HasMaxLength(6);
            }
        }
    }
}
