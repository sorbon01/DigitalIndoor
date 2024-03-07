using DigitalIndoorAPI.Models.Abstraction;

namespace DigitalIndoorAPI.Models
{
    public class TerminalLog:DbRecord
    {
        public int? TerminalId { get; set; }
        public Terminal Terminal { get; set; }
        public string Data { get; set; }
    }
}
