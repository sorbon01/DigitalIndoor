using DigitalIndoor.DTOs;
using DigitalIndoor.DTOs.Params;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Models.Common;

namespace DigitalIndoor.Services
{
    public interface IUserService
    {
        Task<PagedList<User, UserViewDto>> SearchAsync(NameAndPagedParam param);
        Task<UserViewDto> AddAsync(UserCreateDto create);
        Task<UserViewDto> UpdateAsync(UserUpdateDto update);
        Task<UserViewDto> DeleteAsync(int id);
        Task<UserInfoDto> GetUserInfoAsync();
        Task<User> GetCurrentAsync();
        Task<UserViewDto> ChangePasswordAsync(ChangePasswordDto changeObj, string username);
    }
}
