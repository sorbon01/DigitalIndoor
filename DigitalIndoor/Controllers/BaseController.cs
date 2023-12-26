using DigitalIndoor.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoor.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(MessageViewDto), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(List<ValidationErrorDto>), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {
        protected string Username
            =>  User.Identity.Name;
            
    }
}
