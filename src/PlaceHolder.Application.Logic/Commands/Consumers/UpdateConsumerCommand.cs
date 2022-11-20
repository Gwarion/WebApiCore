using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class UpdateConsumerCommand : ICommand<Consumer>
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}
