using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SaudiExpress.Database.EntityModels
{
    public class AppRole : IdentityRole
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
