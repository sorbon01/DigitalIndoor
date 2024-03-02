using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Models;

namespace DigitalIndoorAPI.DTOs.Request
{
    public class OrderCreateDto
    {
        public string ClientName { get; set; }
        public int VideoId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int[] PlayListIds { get; set; }
    }
}
