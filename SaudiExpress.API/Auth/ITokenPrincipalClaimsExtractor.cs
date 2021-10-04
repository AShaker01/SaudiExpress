using System.Security.Claims;

namespace SaudiExpress.API.Auth
{
    public interface ITokenPrincipalClaimsExtractor
    {
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
