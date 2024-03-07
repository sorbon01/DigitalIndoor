using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoorAPI.Controllers
{
    public class UserController : CrudBaseController<IUserService, User, UserViewDto, UserParam, UserCreateDto, UserUpdateDto>
    {
        public UserController(IUserService userService):base(userService) { }

        [HttpGet("info")]
        public async Task<ActionResult<UserInfoDto>> GetInfoAsync()
             => Ok(await service.GetUserInfoAsync());

        [HttpPut("change-password")]
        public async Task<ActionResult<UserViewDto>> ChangePasswordAsync([FromBody] ChangePasswordDto change)
            => Ok(await service.ChangePasswordAsync(change));


    }
}
