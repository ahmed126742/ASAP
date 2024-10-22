using System.Security.Claims;

namespace ASAP_Task.Infrastructure.Helper
{
    public sealed class UserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
                _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

        public string UserName => _httpContextAccessor
           .HttpContext?
           .User?
           .Identity?
           .Name ??
       throw new ApplicationException("User context is unavailable");
    }

    internal static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claims)
        {
            string? userId = claims?.FindFirst("id")?.Value;

            if (userId == null)
                throw new Exception("User Id Is unavailable");

            return int.Parse(userId);
        }
    }
}
