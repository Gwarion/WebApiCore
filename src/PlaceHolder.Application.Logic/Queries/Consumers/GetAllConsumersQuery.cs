using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;
using System.Collections.Generic;

namespace PlaceHolder.Application.Logic.Queries.Consumers
{
    public class GetAllConsumersQuery : IQuery<List<Consumer>>
    {
        public GetAllConsumersQuery() { }
    }
}
