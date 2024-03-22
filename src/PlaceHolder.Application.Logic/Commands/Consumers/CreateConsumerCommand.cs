using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;

namespace PlaceHolder.Application.Logic.Commands.Consumers;

public sealed record CreateConsumerCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    Address Address)
    : ICommand<Guid> { }

