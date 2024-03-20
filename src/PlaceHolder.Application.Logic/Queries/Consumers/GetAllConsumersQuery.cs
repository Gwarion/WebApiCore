using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Collections.Generic;

namespace PlaceHolder.Application.Logic.Queries.Consumers;

public sealed record GetAllConsumersQuery : IQuery<List<Consumer>> { }

