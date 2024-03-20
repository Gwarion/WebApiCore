using MediatR;
using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Application.Services.Ports.EF;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Cqrs.MediatrPipeline
{
    public class DbTransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private IDbContext _context;

        public DbTransactionPipelineBehavior(IDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return request is IQuery<TResponse> ? await next() : await _context.ExecuteAsTransaction(async () => await next());
        }
    }
}
