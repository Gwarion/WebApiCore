using PlaceHolder.Application.Services.Ports.Cqrs;
using System;
using System.Collections.Generic;

namespace PlaceHolder.Application.Logic.AsyncCommands
{
    public class ConsumerNameCatchUpCommand : IAsyncCommand
    {
        public List<Guid> ConsumerIds { get; }

        public ConsumerNameCatchUpCommand(List<Guid> consumerIds)
            => ConsumerIds = consumerIds;
    }
}
