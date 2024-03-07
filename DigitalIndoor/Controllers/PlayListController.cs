using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Services;

namespace DigitalIndoorAPI.Controllers
{
    public class PlayListController : CrudBaseController<IPlayListService, PlayList, PlayListViewDto, PlayListParam, PlayListCreateDto, PlayListUpdateDto>
    {
        public PlayListController(IPlayListService service) : base(service) { }
    }
}
