using AutoMapper;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Exceptions;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Models.DB;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoorAPI.Services.Implementations
{
    public class PlayListService : IPlayListService
    {
        readonly IMapper mapper;
        readonly Context context;
        readonly IUserService userService;
        public PlayListService(IMapper mapper, Context context, IUserService userService)
        {
            this.mapper = mapper;
            this.context = context;
            this.userService = userService;
        }

        public async Task<PagedList<PlayList, PlayListViewDto>> SearchAsync(PlayListParam param)
        {
            var query = context.PlayLists
                .Include(x => x.User)
                .Where(x => !x.IsDeleted &&
                (string.IsNullOrWhiteSpace(param.Name) || x.Name.Contains(param.Name)))
                .OrderBy(x => x.Id).AsQueryable();

            var count = query.CountAsync();
            var items = query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<PlayList, PlayListViewDto>(await items, await count, param.Page, param.Size, mapper);
        }
        public async Task<PlayListViewDto> AddAsync(PlayListCreateDto create)
        {
            if (context.PlayLists.Any(c => !c.IsDeleted && c.Name == create.Name))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            var playlist = mapper.Map<PlayList>(create);
            var user = await userService.GetCurrentAsync();
            playlist.UserId = user.Id;

            await context.PlayLists.AddAsync(playlist);
            await context.SaveChangesAsync();

            playlist.User = user;
            return mapper.Map<PlayListViewDto>(playlist);
        }
        public async Task<PlayListViewDto> UpdateAsync(PlayListUpdateDto update)
        {
            var playlist = context.PlayLists
                .Include(x=>x.User)
                .FirstOrDefault(c => !c.IsDeleted && c.Id == update.Id);
            if (playlist is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            if (context.PlayLists.Any(c => !c.IsDeleted && c.Id != update.Id && c.Name == update.Name))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            playlist.Name = update.Name;
            await context.SaveChangesAsync();
            return mapper.Map<PlayListViewDto>(playlist);
        }
        public async Task<PlayListViewDto> DeleteAsync(int id)
        {
            var playlist = await context.PlayLists.Include(x=>x.User).FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);
            if (playlist is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            playlist.IsDeleted = true;
            await context.SaveChangesAsync();

            return mapper.Map<PlayListViewDto>(playlist);
        }
    }
}
