using AutoMapper;
using MediatR;
using PlaceHolder.API.Controllers.Consumers;
using PlaceHolder.API.Controllers.Consumers.Dtos;
using PlaceHolder.Application.Logic.Commands.Consumers;
using PlaceHolder.Application.Logic.Queries.Consumers;
using PlaceHolder.Application.Services.Ports.Cqrs;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaceHolder.DrivingAdapter.WebApi.Consumers
{
    public class ConsumerService : IConsumerService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ConsumerService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        public async Task<ConsumerDto> CreateConsumerAsync(ConsumerDto consumerDto)
        {
            var command = _mapper.Map<ConsumerDto, CreateConsumerCommand>(consumerDto);
            var res = await _mediator.Send(command);

            return _mapper.Map<Consumer, ConsumerDto>(res);
        }

        public async Task<ConsumerDto> UpdateConsumerAsync(ConsumerDto consumerDto)
        {
            var command = _mapper.Map<ConsumerDto, UpdateConsumerCommand>(consumerDto);
            var res = await _mediator.Send(command);

            return _mapper.Map<Consumer, ConsumerDto>(res);
        }

        public async Task<ConsumerDto> GetOneByIdAsync(string guid)
        {
            var query = new GetOneConsumerByIdQuery(guid);

            return _mapper.Map<Consumer, ConsumerDto>(await _mediator.Send(query));
        }

        public async Task<List<ConsumerDto>> GetAllAsync()
        {
            var query = new GetAllConsumersQuery();

            return _mapper.Map<List<Consumer>, List<ConsumerDto>>(await _mediator.Send(query));
        }
    }
}
