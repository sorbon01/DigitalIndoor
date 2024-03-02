using AutoMapper;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Exceptions;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoorAPI.Services.Implementations
{
    public class VideoService : IVideoService
    {
        readonly IUserService userService;
        readonly Context context;
        readonly IMapper mapper;

        public VideoService(IUserService userService, Context context, IMapper mapper)
        {
            this.userService = userService;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PagedList<Video, VideoViewDto>> SearchAsync(VideoParam param)
        {
            param.ToDate = param.ToDate ?? param.ToDate.Value.AddDays(1);

            var query = context.Videos
                .Include(x => x.User)
                .Where(x => !x.IsDeleted &&
                (string.IsNullOrWhiteSpace(param.Name) || x.Name.Contains(param.Name)) &&
                (!param.FromDate.HasValue || x.RecordDate >= param.FromDate) &&
                (!param.ToDate.HasValue || x.RecordDate < param.ToDate))
                .OrderBy(x => x.Id).AsQueryable();
            
            var count = query.CountAsync();
            var items = query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<Video, VideoViewDto>(await items,await count, param.Page, param.Size, mapper);
        }
        public async Task<VideoViewDto> AddAsync(VideoCreateDto create)
        {
            if (create.File == null || create.File.Length <= 0)
                throw new ToException(ToErrors.FILE_IS_EMPTY);

            var user = await userService.GetCurrentAsync();

            var video = new Video()
            {
                Name = create.Name,
                Size = create.File.Length,
                UserId = user.Id,
                IsDeleted = true
            };

            await context.Videos.AddAsync(video);
            await context.SaveChangesAsync();

            var fileName = $"{video.Id}{Path.GetExtension(create.File.FileName)}";

            string storageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
            if (!Directory.Exists(storageDirectory))
                Directory.CreateDirectory(storageDirectory);

            var fileFullPath = Path.Combine(storageDirectory, fileName);

            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await create.File.CopyToAsync(stream);
            }

            video.FileName = fileName;
            video.IsDeleted = false;
            await context.SaveChangesAsync();
            video.User = user;

            return mapper.Map<VideoViewDto>(video);
        }
        public async Task<VideoViewDto> UpdateAsync(VideoUpdateDto update)
        {
            var video = await context.Videos
                .Include(v => v.User)
                .FirstOrDefaultAsync(x=> x.Id == update.Id);
            if (video is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            video.Name = update.Name;
            await context.SaveChangesAsync();
            return mapper.Map<VideoViewDto>(video);
        }
        public async Task<VideoViewDto> DeleteAsync(int id)
        {
            var video = await context.Videos
                .Include(v=> v.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (video is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            video.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<VideoViewDto>(video);
        }
    }
}
