using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities.TimeStampedEntity;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities
{
    public class Address : TimeStampedEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }

        internal class AddressConfiguration : TimeStampedEntityConfiguration<Address>
        {
            public override void Configure(EntityTypeBuilder<Address> builder)
            {
                base.Configure(builder);
            }
        }
    }
}
