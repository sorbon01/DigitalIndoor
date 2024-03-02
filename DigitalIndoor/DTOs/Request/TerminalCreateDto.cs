using DigitalIndoorAPI.Models;

namespace DigitalIndoorAPI.DTOs.Request
{
    public class TerminalCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Pincode { get; set; }
        public int PlayListId { get; set; }
        public bool IsActive { get; set; }
    }
}
