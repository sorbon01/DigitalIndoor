using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Services;

namespace DigitalIndoorAPI.Controllers
{
    public class TerminalController : CrudBaseController<ITerminalService, Terminal, TerminalViewDto, TerminalParam, TerminalCreateDto, TerminalUpdateDto>
    {
        public TerminalController(ITerminalService service) : base(service) { }
    }
}
