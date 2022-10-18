using MediatR;
using PlaceHolder.Application.Services.Ports.Cqrs;
using System;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.Cqrs.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IMediator _mediator;

        public QueryDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
        {
            try
            {
                if (query == null) throw new ArgumentNullException(nameof(query), "command can not be null.");

                return await _mediator.Send(query);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }
    }
}
