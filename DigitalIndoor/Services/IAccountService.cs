using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;

namespace DigitalIndoor.Services
{
    public interface IAccountService
    {
        JsonWebTokenDto SignIn(SignInDto signInObj);
        JsonWebTokenDto RefreshAccessToken(string refreshToken);
        void RevokeRefreshToken(string username);
    }
}
