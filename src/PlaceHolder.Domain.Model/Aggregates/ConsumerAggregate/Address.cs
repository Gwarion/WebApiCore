using PlaceHolder.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate
{
    public class Address : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }
    }
}
