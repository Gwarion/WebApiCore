using PlaceHolder.Application.Services.Cqrs.Queries;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.Queries.Consumers
{
    public class GetOneConsumerByIdQueryHandler : AbstractQueryHandler<GetOneConsumerByIdQuery, Consumer>
    {
        private readonly IConsumerRepository _consumerRepository;

        public GetOneConsumerByIdQueryHandler(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        public override async Task<Consumer> Handle(GetOneConsumerByIdQuery request)
        {
            return await _consumerRepository.GetOneByIdAsync(request.Guid);
        }
    }
}
