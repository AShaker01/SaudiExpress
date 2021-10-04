using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiExpress.API.Auth
{
    public class JwtMetadata
    {
        public string Id { get; set; }
        public string AuthToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
