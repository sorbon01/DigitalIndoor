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
        Task<UserViewDto> GetByIdAsync(int id);
        Task<UserInfoDto> GetUserInfoAsync(string username);
        Task<User> GetByUsernameAsync(string username);
        Task<UserViewDto> ChangePasswordAsync(ChangePasswordDto changeObj, string username);
    }
}
