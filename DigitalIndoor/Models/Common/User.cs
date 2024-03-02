using DigitalIndoorAPI.Models.Abstraction;

namespace DigitalIndoorAPI.Models.Common
{
    public class User : DbRecord
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
