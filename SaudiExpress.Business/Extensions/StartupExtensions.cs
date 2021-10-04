using Microsoft.Extensions.DependencyInjection;
using SaudiExpress.Business.IServices;
using SaudiExpress.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Business.Extensions
{
    public static class StartupExtensions
    {
        public static void AddBusinessLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
