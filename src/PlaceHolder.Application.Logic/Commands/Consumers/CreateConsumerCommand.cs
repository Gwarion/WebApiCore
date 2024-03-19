using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;

namespace PlaceHolder.Application.Logic.Commands.Consumers;

public sealed record CreateConsumerCommand(string FirstName, string LastName, Address Address)
    : ICommand<Consumer>{ }

