using MediatR;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.AsyncCommands
{
    public class ConsumerNameCatchUpCommandHandler : IRequestHandler<ConsumerNameCatchUpCommand>
    {
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerNameCatchUpCommandHandler(IConsumerRepository consumerRepository)
            => _consumerRepository = consumerRepository;

        public async Task Handle(ConsumerNameCatchUpCommand command, CancellationToken cancellationToken)
        {
            foreach(var id in command.ConsumerIds)
            {
                var consumer = await _consumerRepository.GetOneByIdAsync(id);

                if(string.IsNullOrWhiteSpace(consumer.FirstName))
                {
                    consumer.FirstName = "UNKOWN";
                }

                if (string.IsNullOrWhiteSpace(consumer.LastName))
                {
                    consumer.LastName = "UNKOWN";
                }

                _ = await _consumerRepository.UpdateAsync(consumer);
            }
        }
    }
}
