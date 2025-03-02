using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WestoniaAPI.Controllers.v1
{
    /// <summary>
    /// Base controller for the Westonia API, which contains common services and methods for all controllers.
    /// </summary>
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WestoniaAPIBaseController : ControllerBase
    {
        // TODO: Add access to the logger, mapper, and other common services here
    }
}
