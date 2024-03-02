using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.Services;

namespace DigitalIndoorAPI.Services
{
    public interface IUserService:ICrudService<User, UserViewDto, UserParam, UserCreateDto, UserUpdateDto>
    {
        Task<UserInfoDto> GetUserInfoAsync();
        Task<User> GetCurrentAsync();
        Task<UserViewDto> ChangePasswordAsync(ChangePasswordDto changeObj);
    }
}
