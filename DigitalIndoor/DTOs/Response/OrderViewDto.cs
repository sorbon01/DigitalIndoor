namespace DigitalIndoorAPI.DTOs.Response
{
    public class OrderViewDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<PlayListShortViewDto> PlayLists { get; set; }
    }
}
