using AutoMapper;
using PlaceHolder.API.Controllers.Consumers;
using PlaceHolder.API.Controllers.Consumers.Dtos;
using PlaceHolder.Application.Logic.Commands.Consumers;
using PlaceHolder.Application.Logic.Queries.Consumers;
using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Threading.Tasks;

namespace PlaceHolder.DrivingAdapter.WebApi.Consumers
{
    public class ConsumerService : IConsumerService
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IMapper _mapper;

        public ConsumerService(ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IMapper mapper)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _mapper = mapper;
        }

        public async Task<ConsumerDto> CreateConsumerAsync(ConsumerDto consumerDto)
        {
            var command = _mapper.Map<ConsumerDto, CreateConsumerCommand>(consumerDto);
            var res = await _commandDispatcher.DispatchAsync(command);

            return _mapper.Map<Consumer, ConsumerDto>(res);
        }

        public async Task<ConsumerDto> UpdateConsumerAsync(ConsumerDto consumerDto)
        {
            var command = _mapper.Map<ConsumerDto, UpdateConsumerCommand>(consumerDto);
            var res = await _commandDispatcher.DispatchAsync(command);

            return _mapper.Map<Consumer, ConsumerDto>(res);
        }

        public async Task<ConsumerDto> GetOneByIdAsync(string guid)
        {
            var query = new GetOneConsumerByIdQuery(guid);

            return _mapper.Map<Consumer, ConsumerDto>(await _queryDispatcher.DispatchAsync(query));
        }

    }
}
