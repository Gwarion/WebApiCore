using Microsoft.AspNetCore.Mvc;
using PlaceHolder.API.Version;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceHolder.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    public abstract class PlaceHolderController : ControllerBase
    {
    }
}
