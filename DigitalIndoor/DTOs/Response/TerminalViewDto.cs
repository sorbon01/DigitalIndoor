using DigitalIndoorAPI.Models;

namespace DigitalIndoorAPI.DTOs.Response
{
    public class TerminalViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Pincode { get; set; }
        public int? PlayListId { get; set; }
        public string PlayListName { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
