namespace DigitalIndoorAPI.DTOs.Response
{
    public class UserInfoDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string[] Functionals { get; set; }
    }
}
