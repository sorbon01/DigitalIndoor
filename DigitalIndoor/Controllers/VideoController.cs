using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using DigitalIndoorAPI.Controllers.Base;

namespace DigitalIndoorAPI.Controllers
{
    [ProducesResponseType(typeof(RoleViewDto), StatusCodes.Status200OK)]
    public class VideoController : BaseController
    {
        readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
            => this.videoService = videoService;

        [HttpGet]
        [ProducesResponseType(typeof(PagedList<Role, RoleViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] VideoParam param)
            => Ok(await videoService.SearchAsync(param));

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] VideoCreateDto create)
            => Ok(await videoService.AddAsync(create));

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] VideoUpdateDto update)
            => Ok(await videoService.UpdateAsync(update));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
             => Ok(await videoService.DeleteAsync(id));

    }
}
