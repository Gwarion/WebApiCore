using PlaceHolder.Application.Services.Cqrs.Commands;
using PlaceHolder.Application.Services.Ports.Kafka;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate.Events;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class UpdateConsumerCommandHandler : AbstractCommandHandler<UpdateConsumerCommand, Consumer>
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IKafkaProducer _kafkaProducer;

        public UpdateConsumerCommandHandler(IConsumerRepository consumerRepository, IKafkaProducer kafkaProducer)
        {
            _consumerRepository = consumerRepository;
            _kafkaProducer = kafkaProducer;
        }

        protected override async Task<Consumer> Handle(UpdateConsumerCommand request)
        {
            var consumer = await _consumerRepository.GetOneByIdAsync(request.Guid);

            consumer.FirstName = request.FirstName;
            consumer.LastName = request.LastName;

            consumer.Address.City = request.Address.City;
            consumer.Address.Street = request.Address.Street;
            consumer.Address.Country = request.Address.Country;
            consumer.Address.PostalCode = request.Address.PostalCode;

            var updatedConsumer = await _consumerRepository.UpdateAsync(consumer);

            await _kafkaProducer.ProduceAsync(new ConsumerUpdatedEvent(updatedConsumer));

            return updatedConsumer;
        }
    }
}
