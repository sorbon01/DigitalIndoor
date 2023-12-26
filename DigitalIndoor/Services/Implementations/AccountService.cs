using AutoMapper;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Exceptions;
using DigitalIndoor.Models.Common;
using DigitalIndoor.Models.DB;
using DigitalIndoor.Models.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DigitalIndoor.Services.Implementations
{
    public class AccountService : IAccountService
    {
        readonly JwtOptions options;
        readonly IPasswordHasher<BaseUser> passwordHasher;
        readonly Context context;
        readonly IMapper mapper;
        readonly IServiceProvider serviceProvider;


        public AccountService(
            IOptions<JwtOptions> options,
            IPasswordHasher<BaseUser> passwordHasher,
            Context context,
            IMapper mapper,
            IServiceProvider serviceProvider
            )
        {
            this.options = options.Value;
            this.passwordHasher = passwordHasher;
            this.context = context;
            this.mapper = mapper;
            this.serviceProvider = serviceProvider;
        }
        public JsonWebTokenDto SignIn(SignInDto signInObj)
        {
            var user = context.Users
                .Where(x => !x.IsDeleted && x.Username == signInObj.Username)
                .Select(u => mapper.Map<BaseUser>(u))
                .SingleOrDefault();
            if (user is null || !BCrypt.Net.BCrypt.Verify(signInObj.Password, user.Password))
                throw new ToException(ToErrors.INVALID_CREDENTIALS);

            var jwt = generateJWT(user);

            var refreshTokenObj = context.RefreshTokens.SingleOrDefault(x => x.Username == user.Username);
            if (refreshTokenObj is not null)
                refreshTokenObj.Refresh(jwt.RefreshToken, options.ExpiryRefreshTokenDays);
            else
                context.Add(new RefreshToken(user.Username, jwt.RefreshToken, options.ExpiryRefreshTokenDays));
            
            context.SaveChanges();
            return jwt;
        }
        public JsonWebTokenDto RefreshAccessToken(string refreshToken)
        {
            var refreshTokenObj = context.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);
            if (refreshTokenObj is null)
                throw new ToException(ToErrors.REFRESH_TOKEN_NOT_FOUND);
            if(refreshTokenObj.Expires  < DateTime.Now)
            {
                context.Remove(refreshTokenObj);
                context.SaveChanges();
                throw new ToException(ToErrors.EXPIRED_REFRESH_TOKEN);
            }

            var user = context.Users
                .Where(u => !u.IsDeleted && u.Username == refreshTokenObj.Username)
                .Select(u => mapper.Map<BaseUser>(u))
                .SingleOrDefault();

            if (user is null)
                throw new ToException(ToErrors.USER_NOT_FOUND);

            var jwt = generateJWT(user);

            refreshTokenObj.Refresh(jwt.RefreshToken, options.ExpiryRefreshTokenDays);
            context.SaveChanges();
            return jwt;
        }
        public void RevokeRefreshToken(string username)
        {
            var refreshTokenObj = context.RefreshTokens.SingleOrDefault(x => x.Username == username);
            if (refreshTokenObj is not null)
            {
                context.Remove(refreshTokenObj);
                context.SaveChanges();
            }
        }

        public void SignOut()
        {
            using(var scope = serviceProvider.CreateScope())
            {
                var tokenManager = scope.ServiceProvider.GetService<ITokenManager>();
                var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
                var username = httpContextAccessor.HttpContext.User.Identity.Name;
                RevokeRefreshToken(username);
                tokenManager.DeactivateCurrentAsync().Wait();
            }
        }

        JsonWebTokenDto generateJWT(BaseUser user)
        {
            var expires = DateTime.UtcNow.AddMinutes(options.ExpiryMinutes);
            var exp = (long)new TimeSpan(expires.Ticks - new DateTime(1970, 1, 1).Ticks).TotalSeconds;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("unique_name", user.Username),
            };
            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                claims: userClaims,
                expires: expires,
                signingCredentials: credentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var refreshToken = passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
            return new JsonWebTokenDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Expires = exp
            };


        }

    }
}
