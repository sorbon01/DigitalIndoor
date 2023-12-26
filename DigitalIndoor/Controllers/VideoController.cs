using DigitalIndoor.DTOs.Params;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.DTOs;
using DigitalIndoor.Models.Common;
using DigitalIndoor.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalIndoor.Controllers
{
    public class VideoController : BaseController
    {
        readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
            => this.videoService = videoService;

        [HttpGet]
        [ProducesResponseType(typeof(PagedList<Role, RoleViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] NameDatePagedParam param)
            => Ok(await videoService.SearchAsync(param));

        [HttpPost]
        [ProducesResponseType(typeof(RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromForm] VideoCreateDto create)
            => Ok(await videoService.AddAsync(create, Username));

        [HttpPut]
        [ProducesResponseType(typeof(RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] VideoUpdateDto update)
            => Ok(await videoService.UpdateAsync(update));

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RoleViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync(int id)
             => Ok(await videoService.DeleteAsync(id));

    }
}
