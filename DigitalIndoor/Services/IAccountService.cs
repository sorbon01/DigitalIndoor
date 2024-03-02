using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;

namespace DigitalIndoorAPI.Services
{
    public interface IAccountService
    {
        JsonWebTokenDto SignIn(SignInDto signInObj);
        JsonWebTokenDto RefreshAccessToken(string refreshToken);
        void RevokeRefreshToken(string username);
        void SignOut();
        string Username();

    }
}
