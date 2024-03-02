using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoorAPI.Controllers
{
    public class RoleController : CrudBaseController<IRoleService, Role, RoleViewDto, RoleParam, RoleCreateDto, RoleUpdateDto>
    {
        public RoleController(IRoleService service) : base(service)
        {
        }

        [HttpGet("functionals")]
        public ActionResult<List<string>> GetFunctionals()
            => Ok(service.GetAllFunctionals());

    }
}
