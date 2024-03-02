namespace DigitalIndoorAPI.Models.Options
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
        public int ExpiryRefreshTokenDays { get; set; }

    }
}
