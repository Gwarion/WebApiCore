using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities
{
    public class ConsumerEntity : TimeStampedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #region navigation properties

        public AddressEntity Address { get; set; }

        #endregion


        internal class ConsumerEntityConfiguration : TimeStampedEntityConfiguration<ConsumerEntity>
        {
            public override void Configure(EntityTypeBuilder<ConsumerEntity> builder)
            {
                base.Configure(builder);

                builder.ToTable("Consumer");

                builder.HasOne(consumer => consumer.Address)
                    .WithOne()
                    .HasForeignKey<AddressEntity>(address => address.ConsumerId)
                    .IsRequired();

                builder.Property(consumer => consumer.FirstName)
                    .HasMaxLength(50);

                builder.Property(consumer => consumer.LastName)
                    .HasMaxLength(50);

                builder.Property(consumer => consumer.PhoneNumber)
                    .HasMaxLength(12);
    
                builder.Property(consumer => consumer.Email)
                    .HasMaxLength(100);
            }
        }
    }
}
