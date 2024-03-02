using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;

namespace DigitalIndoorAPI.Services
{
    public interface ITerminalService : ICrudService<Terminal,TerminalViewDto,TerminalParam,TerminalCreateDto,TerminalUpdateDto>
    {
    }
}
