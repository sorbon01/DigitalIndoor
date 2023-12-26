namespace DigitalIndoor.DTOs.Params
{
    public class DateAndPaged : PagedParam
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
