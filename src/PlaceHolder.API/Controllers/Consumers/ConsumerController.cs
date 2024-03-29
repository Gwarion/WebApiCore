using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceHolder.API.Controllers.Consumers.Resources;
using PlaceHolder.Application.Logic.Commands.Consumers;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using PlaceHolder.QueryModel.Consumers;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PlaceHolder.API.Controllers.Consumers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ConsumerController : PlaceHolderController
    {
        private readonly IConsumerQueryRepository _queryRepository;

        public ConsumerController(IMediator mediator, IMapper mapper, IConsumerQueryRepository queryRepository) : base(mediator, mapper) 
            => _queryRepository = queryRepository;

        [HttpPost(Name = "CreateConsumer")]
        [ProducesResponseType(typeof(ConsumerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateConsumer([FromBody] ConsumerResource resource)
        {
            var command = _mapper.Map<ConsumerResource, CreateConsumerCommand>(resource);
            var consumerId = await _mediator.Send(command);

            return new CreatedResult(string.Empty, await _queryRepository.GetOneByIdAsync(consumerId));
        }

        [HttpPut("{consumerId}", Name = "UpdateConsumer")]
        [ProducesResponseType(typeof(ConsumerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateConsumer([FromRoute] Guid consumerId, [FromBody] ConsumerResource resource)
        {
            var command = _mapper.Map<ConsumerResource, UpdateConsumerCommand>(resource);
            command.ConsumerId = consumerId;

            await _mediator.Send(command);

            return Ok(await _queryRepository.GetOneByIdAsync(consumerId));
        }

        [HttpGet("{consumerId}", Name = "GetOne")]
        [ProducesResponseType(typeof(ConsumerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOneConsumer([FromRoute] Guid consumerId)
        {
            return Ok(await _queryRepository.GetOneByIdAsync(consumerId));
        }

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(ConsumerResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllConsumer([FromQuery] int limit = 10)
        {
            return Ok(await _queryRepository.GetAllAsync(limit));
        }
    }
}