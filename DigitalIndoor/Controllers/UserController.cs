using DigitalIndoor.DTOs;
using DigitalIndoor.DTOs.Params;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Models.Common;
using DigitalIndoor.Services;
using DigitalIndoor.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoor.Controllers
{
    public class UserController : BaseController
    {
        readonly IUserService userService;

        public UserController(IUserService userService)
            => this.userService = userService;


        [HttpGet]
        [ProducesResponseType(typeof(PagedList<User, UserViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] NameAndPagedParam param)
            => Ok(await userService.SearchAsync(param));

        [HttpPost]
        [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] UserCreateDto create)
            => Ok(await userService.AddAsync(create));

        [HttpPut]
        [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDto update)
            => Ok(await userService.UpdateAsync(update));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(int id)
            => Ok(await userService.GetByIdAsync(id));

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int id)
             => Ok(await userService.DeleteAsync(id));

        [HttpGet("info")]
        [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfoAsync()
             => Ok(await userService.GetUserInfoAsync(Username));

        [HttpPut("change-password")]
        [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto change)
            => Ok(await userService.ChangePasswordAsync(change, Username));


    }
}
