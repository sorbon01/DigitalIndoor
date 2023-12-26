namespace DigitalIndoor.DTOs.Request
{
    public class UserCreateDto
    {
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
