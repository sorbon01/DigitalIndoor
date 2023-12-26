using DigitalIndoor.DTOs.Params;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.DTOs;
using DigitalIndoor.Models.Common;
using DigitalIndoor.Models;

namespace DigitalIndoor.Services
{
    public interface IVideoService
    {
        Task<PagedList<Video, VideoViewDto>> SearchAsync(NameDatePagedParam param);
        Task<VideoViewDto> AddAsync(VideoCreateDto create, string username);
        Task<VideoViewDto> UpdateAsync(VideoUpdateDto update);
        Task<VideoViewDto> DeleteAsync(int id);
    }
}
