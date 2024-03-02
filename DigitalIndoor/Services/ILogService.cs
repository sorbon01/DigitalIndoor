namespace DigitalIndoorAPI.Services
{
    public interface ILogService
    {
        void Log(string text);
        void Crash(string text);
    }
}
