using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PlaceHolder.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public abstract class PlaceHolderController : ControllerBase 
    {
        protected readonly IMediator _mediator;
        protected readonly IMapper _mapper;

        protected PlaceHolderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
