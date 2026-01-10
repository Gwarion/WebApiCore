using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceHolder.API.Controllers.Consumers.Resources;
using PlaceHolder.Application.Logic.Commands.Consumers;
using PlaceHolder.QueryModel.Consumers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.API.Controllers.Consumers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ConsumerController : PlaceHolderController
    {
        private const int ChunkSize = 10_000;
        private const char Separator = ',';
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

        [HttpGet("data-stream", Name = "GetAllJsonStreamedConsumers")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async IAsyncEnumerable<IReadOnlyList<ConsumerDto>> GetAllJsonStreamedConsumers([EnumeratorCancellation] CancellationToken cancellationToken)
        {

            int startId = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var chunk = await _queryRepository.GetAllAsync(startId, ChunkSize, cancellationToken) as IReadOnlyList<ConsumerDto>;

                if (!chunk.Any())
                    yield break;

                yield return chunk;

                startId += ChunkSize;
            }
        }

        [HttpGet("csv-data-stream", Name = "GetAllCsvStreamedConsumers")]
        public async Task GetAllCsvStreamedConsumers(CancellationToken cancellationToken)
        {
            Response.StatusCode = StatusCodes.Status200OK;
            Response.ContentType = "text/csv; charset=utf-8";
            Response.Headers.ContentDisposition = "attachment; filename=\"consumers.csv\"";

            await using var writer = new StreamWriter(Response.Body, leaveOpen: true);

            await writer.WriteLineAsync("Guid,FirstName,LastName,Email,PhoneNumber");

            int startId = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var chunk = await _queryRepository.GetAllAsync(startId, ChunkSize, cancellationToken);

                if (!chunk.Any())
                    break;

                foreach (var consumer in chunk)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    await writer.WriteAsync(consumer.Guid.ToString());
                    await writer.WriteAsync(Separator);
                    await writer.WriteAsync(consumer.FirstName ?? string.Empty);
                    await writer.WriteAsync(Separator);
                    await writer.WriteAsync(consumer.LastName ?? string.Empty);
                    await writer.WriteAsync(Separator);
                    await writer.WriteAsync(consumer.Email ?? string.Empty);
                    await writer.WriteAsync(Separator);
                    await writer.WriteLineAsync(consumer.PhoneNumber ?? string.Empty);
                }

                // Flush to ensure client receives partial data and let Kestrel detects disconnects
                await writer.FlushAsync(cancellationToken);

                startId += ChunkSize;
            }
        }
    }
}