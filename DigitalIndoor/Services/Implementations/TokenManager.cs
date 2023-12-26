using DigitalIndoor.Models.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DigitalIndoor.Services.Implementations
{
    public class TokenManager:ITokenManager
    {
        readonly IDistributedCache cache;
        readonly IHttpContextAccessor httpContextAccessor;
        readonly IOptions<JwtOptions> jwtOptions;

        public TokenManager(
                IDistributedCache cache,
                IHttpContextAccessor httpContextAccessor,
                IOptions<JwtOptions> jwtOptions
            )
        {
            this.cache = cache;
            this.httpContextAccessor = httpContextAccessor;
            this.jwtOptions = jwtOptions;
        }
        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
            => await cache.GetStringAsync(GetKey(token)) == null;

        public async Task DeactivateAsync(string token)
            => await cache.SetStringAsync(GetKey(token),
                " ", new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(jwtOptions.Value.ExpiryMinutes)
                });

        string GetCurrentAsync()
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];
            return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Single().Split(" ").Last();
        }

        string GetKey(string token)
            => $"tokens:{token}:deactivated";
    }
}
