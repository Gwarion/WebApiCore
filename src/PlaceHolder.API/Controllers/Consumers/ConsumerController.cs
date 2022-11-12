using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PlaceHolder.API.Controllers.Consumers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ConsumerController : PlaceHolderController
    {
        private readonly IConsumerService _consumerService;
        public ConsumerController(IConsumerService consumerService) => _consumerService = consumerService;


        [HttpPost(Name ="CreateConsumer")]
        [ProducesResponseType(typeof(ConsumerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateConsumer([FromBody] ConsumerDto consumer)
        {
            var result = await _consumerService.CreateConsumerAsync(consumer);
            return new OkObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut(Name = "UpdateConsumer")]
        [ProducesResponseType(typeof(ConsumerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateConsumer([FromBody] ConsumerDto consumer)
        {
            var result = await _consumerService.UpdateConsumerAsync(consumer);
            return new OkObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpGet("{consumerId}", Name = "GetOne")]
        [ProducesResponseType(typeof(ConsumerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOneConsumer([FromRoute] string consumerId)
        {
            var result = await _consumerService.GetOneByIdAsync(consumerId);
            return new OkObjectResult(result) { StatusCode = StatusCodes.Status200OK };
        }
    }
}
