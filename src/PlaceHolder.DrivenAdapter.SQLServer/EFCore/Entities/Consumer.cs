using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities.AbstractEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Entities
{
    public class Consumer : TimeStampedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        internal class ConsumerConfiguration : TimeStampedEntityConfiguration<Consumer>
        {
            public override void Configure(EntityTypeBuilder<Consumer> builder)
            {
                base.Configure(builder);
            }
        }
    }
}
