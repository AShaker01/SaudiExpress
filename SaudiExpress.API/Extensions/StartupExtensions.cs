using CompoundPlating.API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SaudiExpress.API.Auth;
using SaudiExpress.Business.Helpers.Constants;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SaudiExpress.API.Auth.AuthConstants;

namespace SaudiExpress.API.Extensions
{
    public static class StartupExtensions
    {
        private const string SecretKey = "2EA7AC8F3197D23A75D6BA7FBC7A1";
        private static readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public static void AddTokenAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddScoped<ITokenPrincipalClaimsExtractor, TokenPrincipalClaimsExtractor>();

            // Configure JwtIssuerOptions => defining some of the claim properties our generated tokens will contain.
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.JwtValidityInMinutes = Convert.ToDouble(jwtAppSettingOptions[nameof(JwtIssuerOptions.JwtValidityInMinutes)]);
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            // dictate how we want received tokens validated
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // API user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy => policy.RequireClaim(JwtClaimIdentifiers.Roles, Roles.Admin));
                options.AddPolicy("operator", policy => policy.RequireClaim(JwtClaimIdentifiers.Roles, Roles.Operator));
            });
        }
        public static void ConfigureLogging(this IServiceCollection services)
        {
           //TODO:- Config logging
        }
        public static void ConfigureClaimsExtensions(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ClaimsPrincipalService>();
        }
        public static void ConfigureSwagger(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SaudiExpress.API", Version = "v1" });
                    // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                    options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                    // add Security information to each operation for OAuth2
                    options.OperationFilter<SecurityRequirementsOperationFilter>();

                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                        In = ParameterLocation.Header,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "jwt_auth",
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                    {
                         {
                            securityScheme, new string[] { }
                         },
                    };
                    options.AddSecurityRequirement(securityRequirements);
                });
            }
        }
    
    }
}
