using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaudiExpress.Database.EntityConfigurations;
using SaudiExpress.Database.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Database
{
    public class SaudiExpressDatabaseContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>,
                                                         AppUserRole, IdentityUserLogin<string>,
                                                         IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public SaudiExpressDatabaseContext(DbContextOptions<SaudiExpressDatabaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserRoleConfiguration());
            base.OnModelCreating(builder);
        }
        #region Entities
        public DbSet<ProductEntity> Products { get; set; }
        #endregion
    }
}
