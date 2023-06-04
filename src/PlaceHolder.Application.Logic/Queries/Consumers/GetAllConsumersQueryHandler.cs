using PlaceHolder.Application.Logic.AsyncCommands;
using PlaceHolder.Application.Services.Cqrs.Queries;
using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.Queries.Consumers
{
    public class GetAllConsumersQueryHandler : AbstractQueryHandler<GetAllConsumersQuery, List<Consumer>>
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IAsyncCommandDispatcher _dispatcher;

        public GetAllConsumersQueryHandler(IConsumerRepository consumerRepository, IAsyncCommandDispatcher dispatcher)
        {
            _consumerRepository = consumerRepository;
            _dispatcher = dispatcher;
        }

        public override async Task<List<Consumer>> Handle(GetAllConsumersQuery request)
        {
            var allConsumers = await _consumerRepository.GetAllAsync();

            var consumersMissingData = allConsumers
                .Where(c => string.IsNullOrWhiteSpace(c.FirstName)
                            || string.IsNullOrWhiteSpace(c.LastName))
                .Select(c => c.Guid);

            if(consumersMissingData.Any())
            {
                var catchUpCommand = new ConsumerNameCatchUpCommand(consumersMissingData.ToList());
                _ = _dispatcher.Send(catchUpCommand);
            }

            return allConsumers;
        }
    }
}
