namespace DigitalIndoor.Services.Implementations
{
    public class LogService:ILogService
    {
        readonly string dirName = "Logs";
        public void Crash(string text)
            => WriteLog(text, "crash");

        public void Log(string text)
            => WriteLog(text);

        void WriteLog(string text, string category = "log")
        {
            if (!Directory.Exists(dirName))
                try { Directory.CreateDirectory(dirName); } catch { }

            try
            {
                text = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} {text}\n";
                File.AppendAllText($"{dirName}/{category}{DateTime.Now:ddMMyyyy}.txt", text);
            }
            catch { }

        }
    }
}
