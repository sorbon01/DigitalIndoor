using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalIndoorAPI.Controllers
{
    public class AccountController : BaseController
    {
        readonly IAccountService accountService;
        readonly ITokenManager tokenManager;
        readonly IDemoService demoService;

        public AccountController(IAccountService accountService, ITokenManager tokenManager, IDemoService demoService)
        {
            this.accountService = accountService;
            this.tokenManager = tokenManager;
            this.demoService = demoService;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        [ProducesResponseType(typeof(JsonWebTokenDto), StatusCodes.Status200OK)]
        public IActionResult SignIn([FromBody] SignInDto request)
            => Ok(accountService.SignIn(request));

        [AllowAnonymous]
        [HttpPost("token/{refreshToken}/refresh")]
        [ProducesResponseType(typeof(JsonWebTokenDto), StatusCodes.Status200OK)]
        public IActionResult RefreshAccessToken(string refreshToken)
            => Ok(accountService.RefreshAccessToken(refreshToken));

        [HttpPost("sign-out")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void SignOut()
            => accountService.SignOut();
        


        [AllowAnonymous]
        [HttpPost("demo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task CreateDemoAsync()
            => await demoService.CreateDemoAsync();

    }
}
