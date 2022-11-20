using PlaceHolder.Application.Services.Cqrs.Commands;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class CreateConsumerCommandHandler : AbstractCommandHandler<CreateConsumerCommand, Consumer>
    {
        private readonly IConsumerRepository _consumerRepository;

        public CreateConsumerCommandHandler(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;           
        }

        protected override async Task<Consumer> Handle(CreateConsumerCommand request)
        {
            return await _consumerRepository.SaveAsync(new Consumer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address
            });
        }
    }
}
