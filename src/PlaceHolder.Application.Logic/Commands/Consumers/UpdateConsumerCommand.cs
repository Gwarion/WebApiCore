using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;

namespace PlaceHolder.Application.Logic.Commands.Consumers;

public sealed record UpdateConsumerCommand(Guid Guid, string FirstName, string LastName, Address Address)
    : ICommand<Consumer> { }
