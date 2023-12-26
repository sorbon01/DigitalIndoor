
using System.ComponentModel.DataAnnotations;

namespace DigitalIndoor.Models.Common
{
    public class RefreshToken
    {
        public RefreshToken() { }
        public RefreshToken(string username, string token, int expiryByDays)
        {
            Username = username;
            Token = token;
            Expires = DateTime.Now.AddDays(expiryByDays);
        }
        public void Refresh(string token, int expiryByDays)
        {
            Token = token;
            Expires = DateTime.Now.AddDays(expiryByDays);
        }

        [Key]
        public string Username { get; set; }
        public string Token {  get; set; }
        public DateTime Expires { get; set; } 
    }
}
