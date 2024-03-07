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
        readonly IDemoService demoService;

        public AccountController(IAccountService accountService, IDemoService demoService)
        {
            this.accountService = accountService;
            this.demoService = demoService;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public ActionResult<JsonWebTokenDto> SignIn([FromBody] SignInDto request)
            => Ok(accountService.SignIn(request));

        [AllowAnonymous]
        [HttpPost("token/{refreshToken}/refresh")]
        public ActionResult<JsonWebTokenDto> RefreshAccessToken(string refreshToken)
            => Ok(accountService.RefreshAccessToken(refreshToken));

        [HttpPost("sign-out")]
        public void SignnOut()
            => accountService.SignOut();
        


        [AllowAnonymous]
        [HttpPost("demo")]
        public async Task CreateDemoAsync()
            => await demoService.CreateDemoAsync();

    }
}
