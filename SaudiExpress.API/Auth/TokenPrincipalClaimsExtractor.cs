using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SaudiExpress.API.Auth
{
    public class TokenPrincipalClaimsExtractor : ITokenPrincipalClaimsExtractor
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public TokenPrincipalClaimsExtractor(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = _jwtOptions.Audience,
                ValidIssuer = _jwtOptions.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = (SymmetricSecurityKey)_jwtOptions.SigningCredentials.Key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            // we are checking that the algorithm used to sign the token is the one we expect.

            // The reason for this is that in theory someone could create a JWT token and set the signing algorithm to “none”. 
            // The JWT token would be valid (even if unsigned). 
            // This way using a valid refresh token it would be possible to exchange a fake token for a real JWT token.

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
