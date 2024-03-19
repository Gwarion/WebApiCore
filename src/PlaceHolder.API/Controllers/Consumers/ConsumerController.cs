using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceHolder.API.Controllers.Consumers.Resources;
using PlaceHolder.Application.Logic.Commands.Consumers;
using PlaceHolder.Application.Logic.Queries.Consumers;
using PlaceHolder.Domain.Model.Aggregates.ConsumerAggregate;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PlaceHolder.API.Controllers.Consumers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ConsumerController : PlaceHolderController
    {
        public ConsumerController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }

        [HttpPost(Name = "CreateConsumer")]
        [ProducesResponseType(typeof(ConsumerResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateConsumer([FromBody] ConsumerResource resource)
        {
            var command = _mapper.Map<ConsumerResource, CreateConsumerCommand>(resource);
            var res = await _mediator.Send(command);

            return new CreatedResult(string.Empty, _mapper.Map<Consumer, ConsumerResource>(res));
        }

        [HttpPut(Name = "UpdateConsumer")]
        [ProducesResponseType(typeof(ConsumerResource), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateConsumer([FromBody] ConsumerResource resource)
        {
            var command = _mapper.Map<ConsumerResource, UpdateConsumerCommand>(resource);
            var res = await _mediator.Send(command);

            return Ok(_mapper.Map<Consumer, ConsumerResource>(res));
        }

        [HttpGet("{consumerId}", Name = "GetOne")]
        [ProducesResponseType(typeof(ConsumerResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOneConsumer([FromRoute] Guid consumerId)
        {
            var query = new GetOneConsumerByIdQuery(consumerId);
            var res = await _mediator.Send(query);

            return Ok(_mapper.Map<Consumer, ConsumerResource>(res));
        }

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(ConsumerResource), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllConsumer()
        {
            var query = new GetAllConsumersQuery();
            var res = await _mediator.Send(query);

            return Ok(_mapper.Map<List<Consumer>, List<ConsumerResource>>(res));
        }
    }
}