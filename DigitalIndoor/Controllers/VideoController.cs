using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.Models;

namespace DigitalIndoorAPI.Controllers
{
    public class VideoController : CrudBaseController<IVideoService, Video, VideoViewDto, VideoParam, VideoCreateDto, VideoUpdateDto>
    {
        public VideoController(IVideoService videoService):base(videoService) { }
        
    }
}
