using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Database.EntityModels
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RefreshToken { get; set; }
        public bool Active { get; set; }
        public string ResetPasswordToken { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
