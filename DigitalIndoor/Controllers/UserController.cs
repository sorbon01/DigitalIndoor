using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Services;
using DigitalIndoorAPI.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoorAPI.Controllers
{
    [ProducesResponseType(typeof(UserViewDto), StatusCodes.Status200OK)]
    public class UserController : BaseController
    {
        readonly IUserService userService;

        public UserController(IUserService userService)
            => this.userService = userService;


        [HttpGet]
        [ProducesResponseType(typeof(PagedList<User, UserViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] UserParam param)
            => Ok(await userService.SearchAsync(param));

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] UserCreateDto create)
            => Ok(await userService.AddAsync(create));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDto update)
            => Ok(await userService.UpdateAsync(update));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
             => Ok(await userService.DeleteAsync(id));

        [HttpGet("info")]
        [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInfoAsync()
             => Ok(await userService.GetUserInfoAsync());

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto change)
            => Ok(await userService.ChangePasswordAsync(change));


    }
}
