using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace SaudiExpress.API.Auth
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double JwtValidityInMinutes { get; set; }
        public DateTime Expiration => IssuedAt.AddMinutes(JwtValidityInMinutes);
        public DateTime NotBefore => DateTime.UtcNow;
        public DateTime IssuedAt => DateTime.UtcNow;
        public Func<Task<string>> JtiGenerator =>
          () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; }
    }
}
