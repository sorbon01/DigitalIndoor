using DigitalIndoorAPI.Models.Common;

namespace DigitalIndoorAPI.DTOs.Response
{
    public class VideoViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileName { get; set; }
        public string UserFullName { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
