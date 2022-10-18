using PlaceHolder.Application.Services.Cqrs.Commands;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class UpdateConsumerCommandHandler : AbstractCommandHandler<UpdateConsumerCommand, Consumer>
    {
        private readonly IConsumerRepository _consumerRepository;

        public UpdateConsumerCommandHandler(IConsumerRepository consumerRepository)
        {
            _consumerRepository = consumerRepository;
        }

        protected override async Task<Consumer> Handle(UpdateConsumerCommand request)
        {
            var consumer = await _consumerRepository.GetOneByIdAsync(request.Guid);

            consumer.FirstName = request.FirstName;
            consumer.LastName = request.LastName;

            return await _consumerRepository.UpdateAsync(consumer);
        }
    }
}
