using DigitalIndoorAPI.DTOs.Response;

namespace DigitalIndoorAPI.Services
{
    public interface ITerminalLogService
    {
        Task<List<VideoShortViewDto>> GetVideosAsync(TerminalLogParam param)
    }
}
