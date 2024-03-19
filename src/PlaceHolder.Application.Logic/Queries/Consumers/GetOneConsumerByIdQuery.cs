using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;

namespace PlaceHolder.Application.Logic.Queries.Consumers;

public sealed record GetOneConsumerByIdQuery(Guid Guid) : IQuery<Consumer> { }

