using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaudiExpress.Database.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SaudiExpress.Database.UnitOfWorkRepository;

namespace SaudiExpress.Database.Extensions
{
    public static class ServicesExtenions
    {
        public static IServiceCollection _services { get; set; }
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void AddDataLayerServices(this IServiceCollection services, string connectionString, IConfiguration configuration)
        {
            _services = services;
            AddDatabaseContext(connectionString);
            AddIdentityCore();
        }
        private static void AddDatabaseContext(string connectionString)
        {
            _services.AddDbContext<SaudiExpressDatabaseContext>(options =>
            {
                options.UseMySQL(connectionString, builder =>
                {
                    builder.MigrationsHistoryTable("Migrations");
                });
            });
        }
        private static void AddIdentityCore()
        {
            var builder = _services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.User.RequireUniqueEmail = true;
            })
           .AddRoles<AppRole>()
           .AddEntityFrameworkStores<SaudiExpressDatabaseContext>().AddDefaultTokenProviders()
           .AddUserManager<ApplicationUserManager>()
           .AddUserStore<UserStore<AppUser, AppRole, SaudiExpressDatabaseContext, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>>()
           .AddRoleStore<RoleStore<AppRole, SaudiExpressDatabaseContext, string, AppUserRole, IdentityRoleClaim<string>>>();
        }
    }
}
