using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Business.Models.User
{
    public class UserInfoIdentity
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public bool Active { get; set; }
    }
}
