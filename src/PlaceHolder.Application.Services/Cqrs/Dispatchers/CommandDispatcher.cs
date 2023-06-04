using MediatR;
using PlaceHolder.Application.Services.Ports.Cqrs;
using System;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Cqrs.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IMediator _mediator;

        public CommandDispatcher(IMediator mediator)
            => _mediator = mediator;

        public async Task<T> DispatchAsync<T>(ICommand<T> command)
        {
            try
            {
                if (command == null) throw new ArgumentNullException(nameof(command), "command can not be null.");

                return await _mediator.Send(command);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }

        public async Task DispatchAsync(IAsyncCommand command)
        {
            try
            {
                if (command == null) throw new ArgumentNullException(nameof(command), "command can not be null.");

                _ = await _mediator.Send(command);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }
    }
}
