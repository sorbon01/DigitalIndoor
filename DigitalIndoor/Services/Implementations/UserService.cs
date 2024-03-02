using AutoMapper;
using BCrypt.Net;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Exceptions;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Models.DB;
using DigitalIndoorAPI.DTOs.Params;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoorAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        readonly Context context;
        readonly IMapper mapper;
        readonly IAccountService accountService;
        public UserService(Context context, IMapper mapper, IAccountService accountService)
        {
            this.context = context;
            this.mapper = mapper;
            this.accountService = accountService;
        }
        public async Task<PagedList<User, UserViewDto>> SearchAsync(UserParam param)
        {
            var query = context.Users
                .Include(x => x.Role)
                .Where(x => !x.IsDeleted &&
                (string.IsNullOrWhiteSpace(param.FullName) || x.FullName.Contains(param.FullName)))
                .OrderBy(x => x.Id).AsQueryable();

            var count = query.CountAsync();
            var users = query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<User, UserViewDto>(await users, await count, param.Page, param.Size, mapper);
        }
        public async Task<UserViewDto> AddAsync(UserCreateDto create)
        {
            if (await context.Users.AnyAsync(u => !u.IsDeleted && u.Username == create.Username))
                throw new ToException(ToErrors.THIS_USERNAME_ALREADY_EXIST);

            if (!await context.Roles.AnyAsync(u => !u.IsDeleted && u.Id == create.RoleId))
                throw new ToException(ToErrors.ROLE_NOT_FOUND);

            User user = mapper.Map<User>(create);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await context.AddAsync(user);
            await context.SaveChangesAsync();
            await context.Entry(user).Reference(p => p.Role).LoadAsync();
            return mapper.Map<UserViewDto>(user);
        }
        public async Task<UserViewDto> UpdateAsync(UserUpdateDto update)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == update.Id && !u.IsDeleted);
            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            if (await context.Users.AnyAsync(u => !u.IsDeleted && u.Id != update.Id && u.Username == update.Username))
                throw new ToException(ToErrors.THIS_USERNAME_ALREADY_EXIST);

            user.FullName = update.FullName;
            user.Username = update.Username;
            user.Password = string.IsNullOrWhiteSpace(update.Password) ? user.Password : BCrypt.Net.BCrypt.HashPassword(update.Password);
            user.RoleId = update.RoleId;

            await context.SaveChangesAsync();
            context.Entry(user).Reference(p => p.Role).Load();
            accountService.RevokeRefreshToken(user.Username);

            return mapper.Map<UserViewDto>(user);
        }
        public async Task<UserViewDto> DeleteAsync(int id)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            user.IsDeleted = true;
            await context.SaveChangesAsync();
            accountService.RevokeRefreshToken(user.Username);

            return mapper.Map<UserViewDto>(user);
        }
        public async Task<UserViewDto> GetByIdAsync(int id)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .Where(x => !x.IsDeleted && x.Id == id)
                .Select(x => mapper.Map<UserViewDto>(x))
                .FirstOrDefaultAsync();

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);
            return user;
        }
        public async Task<UserInfoDto> GetUserInfoAsync()
        {
            var username = accountService.Username();
            var user = await context.Users
                .Include(x => x.Role)
                .Where(x => !x.IsDeleted && x.Username == username)
                .Select(x => mapper.Map<UserInfoDto>(x))
                .FirstOrDefaultAsync();

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);
            return user;
        }
        public async Task<User> GetCurrentAsync()
        {
            var username = accountService.Username();
            var user = await context.Users
                 .AsNoTracking()
                 .Include(x => x.Role)
                 .FirstOrDefaultAsync(x => !x.IsDeleted && x.Username == username);

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);
            return user;
        }
        public async Task<UserViewDto> ChangePasswordAsync(ChangePasswordDto changeObj)
        {
            var username = accountService.Username();
            var user = await context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.Username == username);

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            if (!BCrypt.Net.BCrypt.Verify(changeObj.OldPassword, user.Password))
                throw new ToException(ToErrors.INVALID_CREDENTIALS);

            user.Password = BCrypt.Net.BCrypt.HashPassword(changeObj.NewPassword);

            await context.SaveChangesAsync();
            context.Entry(user).Reference(p => p.Role).Load();
            return mapper.Map<UserViewDto>(user);
        }


    }
}
