using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.DTOs; 
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Services;
using DigitalIndoorAPI.DTOs.Params;

namespace DigitalIndoorAPI.Services
{
    public interface IVideoService:ICrudService<Video, VideoViewDto, VideoParam, VideoCreateDto, VideoUpdateDto>
    {

    }
}
