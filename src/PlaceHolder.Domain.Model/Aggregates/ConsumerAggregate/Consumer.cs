using PlaceHolder.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate
{
    public class Consumer : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}
