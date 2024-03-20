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

                builder.HasOne(c => c.Address)
                    .WithOne(a => a.Consumer)
                    .HasForeignKey<AddressEntity>(a => a.ConsumerId)
                    .IsRequired();

                builder.Property(c => c.FirstName)
                    .HasMaxLength(50);

                builder.Property(c => c.LastName)
                    .HasMaxLength(50);

                builder.Property(c => c.PhoneNumber)
                    .HasMaxLength(12);
    
                builder.Property(c => c.Email)
                    .HasMaxLength(100);
            }
        }
    }
}
