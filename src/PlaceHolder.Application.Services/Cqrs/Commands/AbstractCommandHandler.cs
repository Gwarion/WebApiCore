using MediatR;
using PlaceHolder.Application.Services.Ports.Cqrs;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Cqrs.Commands
{
    public abstract class AbstractCommandHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : ICommand<TResult>
    {
        Task<TResult> IRequestHandler<TRequest, TResult>.Handle(TRequest request, CancellationToken cancellationToken) => Handle(request);
        
        protected abstract Task<TResult> Handle(TRequest request);
    }

    public abstract class AbstractCommandHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : ICommand
    {
        Task IRequestHandler<TRequest>.Handle(TRequest request, CancellationToken cancellationToken) => Handle(request);

        protected abstract Task Handle(TRequest request);
    }
}
