using MediatR;
using PlaceHolder.Application.Services.Ports.Cqrs;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Cqrs.Queries
{
    public abstract class AbstractQueryHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IQuery<TResult>
    {
        Task<TResult> IRequestHandler<TRequest, TResult>.Handle(TRequest request, CancellationToken cancellationToken) => Handle(request);

        public abstract Task<TResult> Handle(TRequest request);
    }
}
