using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;

namespace PlaceHolder.Application.Logic.Queries.Consumers
{
    public class GetOneConsumerByIdQuery : IQuery<Consumer>
    {
        public Guid Guid { get; set; }

        public GetOneConsumerByIdQuery(string guid)
        {
            Guid = Guid.Parse(guid);
        }
    }
}
