using DigitalIndoor.DTOs;
using DigitalIndoor.DTOs.Params;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Models.Common;
using DigitalIndoor.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoor.Controllers
{
    public class RoleController : BaseController
    {
        readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
            => this.roleService = roleService;


        [HttpGet]
        [ProducesResponseType(typeof(PagedList<Role, RoleViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] NameAndPagedParam param)
            => Ok(await roleService.SearchAsync(param));

        [HttpPost]
        [ProducesResponseType(typeof(RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] RoleCreateDto create)
            => Ok(await roleService.AddAsync(create));

        [HttpPut]
        [ProducesResponseType(typeof(RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] RoleUpdateDto update)
            => Ok(await roleService.UpdateAsync(update));

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int id)
             => Ok(await roleService.DeleteAsync(id));

        [HttpGet("functionals")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        public IActionResult GetFunctionals()
            => Ok(roleService.GetAllFunctionals());

    }
}
