using Microsoft.AspNetCore.Http;
using SaudiExpress.API.Extensions;

namespace CompoundPlating.API.Service
{
    public class ClaimsPrincipalService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClaimsPrincipalService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.GetUserId();
        }
        public string GetCurrentRole()
        {
            return _httpContextAccessor.HttpContext.User.GetUserRole();
        }
    }
}
