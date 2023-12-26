using AutoMapper;
using DigitalIndoor.DTOs;
using DigitalIndoor.DTOs.Params;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Exceptions;
using DigitalIndoor.Models;
using DigitalIndoor.Models.Common;
using DigitalIndoor.Models.DB;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DigitalIndoor.Services.Implementations
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

        public async Task<PagedList<Video, VideoViewDto>> SearchAsync(NameDatePagedParam param)
        {
            param.ToDate = param.ToDate.HasValue ? param.ToDate.Value.AddDays(1) : null;

            var query = context.Videos
                .Include(x => x.User)
                .Where(x => !x.IsDeleted &&
                (string.IsNullOrWhiteSpace(param.Name) || x.Name.Contains(param.Name)) &&
                (!param.FromDate.HasValue || x.RecordDate >= param.FromDate) &&
                (!param.ToDate.HasValue || x.RecordDate < param.ToDate))
                .OrderBy(x => x.Id).AsQueryable();
            
            var count = await query.CountAsync();
            var items = await query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<Video, VideoViewDto>(items, count, param.Page, param.Size, mapper);
        }
        public async Task<VideoViewDto> AddAsync(VideoCreateDto create, string username)
        {
            if (create.File == null || create.File.Length <= 0)
                throw new ToException(ToErrors.FILE_IS_EMPTY);
            
            var video = new Video()
            {
                Name = create.Name,
                Size = create.File.Length,
                User = await userService.GetByUsernameAsync(username),
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

            return mapper.Map<VideoViewDto>(video);
        }
        public async Task<VideoViewDto> UpdateAsync(VideoUpdateDto update)
        {
            var video = await context.Videos.FirstOrDefaultAsync(x=> x.Id == update.Id);
            if (video is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            video.Name = update.Name;
            await context.SaveChangesAsync();
            await context.Entry(video).Reference(x=>x.User).LoadAsync();
            return mapper.Map<VideoViewDto>(video);
        }
        public async Task<VideoViewDto> DeleteAsync(int id)
        {
            var video = await context.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            video.IsDeleted = true;
            await context.SaveChangesAsync();
            await context.Entry(video).Reference(x => x.User).LoadAsync();
            return mapper.Map<VideoViewDto>(video);
        }
    }
}
