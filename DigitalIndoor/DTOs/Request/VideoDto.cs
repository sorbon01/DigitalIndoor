namespace DigitalIndoorAPI.DTOs.Request
{
    public class VideoCreateDto
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }

    }
    public record VideoUpdateDto(int Id, string Name);
}
