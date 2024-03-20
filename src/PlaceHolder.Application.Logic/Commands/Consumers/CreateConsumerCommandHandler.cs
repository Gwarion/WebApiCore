using PlaceHolder.Application.Services.Cqrs.Commands;
using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Events;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class CreateConsumerCommandHandler : AbstractCommandHandler<CreateConsumerCommand, Consumer>
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IKafkaProducer _kafkaProducer;

        public CreateConsumerCommandHandler(IConsumerRepository consumerRepository, IKafkaProducer kafkaProducer)
        {
            _consumerRepository = consumerRepository;
            _kafkaProducer = kafkaProducer;
        }

        protected override async Task<Consumer> Handle(CreateConsumerCommand command)
        {
            var createdConsumer = await _consumerRepository.SaveAsync(new Consumer
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Address = command.Address,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber
            });

            await _kafkaProducer.ProduceAsync(new ConsumerCreatedEvent(createdConsumer));

            return createdConsumer;
        }
    }
}
