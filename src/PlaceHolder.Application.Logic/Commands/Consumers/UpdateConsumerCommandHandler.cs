using PlaceHolder.Application.Services.Cqrs.Commands;
using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Events;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class UpdateConsumerCommandHandler : AbstractCommandHandler<UpdateConsumerCommand>
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IKafkaProducer _kafkaProducer;

        public UpdateConsumerCommandHandler(IConsumerRepository consumerRepository, IKafkaProducer kafkaProducer)
        {
            _consumerRepository = consumerRepository;
            _kafkaProducer = kafkaProducer;
        }

        protected override async Task Handle(UpdateConsumerCommand command)
        {
            var consumer = await _consumerRepository.GetOneByIdAsync(command.ConsumerId);

            consumer.FirstName = command.FirstName;
            consumer.LastName = command.LastName;
            consumer.Email = command.Email;
            consumer.PhoneNumber = command.PhoneNumber;

            consumer.Address.City = command.Address.City;
            consumer.Address.Street = command.Address.Street;
            consumer.Address.Country = command.Address.Country;
            consumer.Address.PostalCode = command.Address.PostalCode;

            var updatedConsumer = await _consumerRepository.UpdateAsync(consumer);

            await _kafkaProducer.ProduceAsync(new ConsumerUpdatedEvent(updatedConsumer));
        }
    }
}
