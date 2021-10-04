using SaudiExpress.Business.Models.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SaudiExpress.API.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(UserInfoIdentity userInfo);
    }
}
