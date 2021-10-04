using SaudiExpress.API.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SaudiExpress.API.Helpers
{
    public class TokensGenerator
    {
        public static async Task<JwtMetadata> GenerateJwt(ClaimsIdentity identity,
          IJwtFactory jwtFactory,
          string userName,
          JwtIssuerOptions jwtOptions)
        {
            return new JwtMetadata
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(userName, identity)
            };
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
