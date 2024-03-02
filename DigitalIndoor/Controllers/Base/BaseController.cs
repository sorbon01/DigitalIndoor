using DigitalIndoorAPI.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoorAPI.Controllers.Base
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(MessageViewDto), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(List<ValidationErrorDto>), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {

    }
}
