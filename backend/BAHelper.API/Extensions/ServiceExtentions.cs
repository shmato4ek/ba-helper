using AutoMapper;
using BAHelper.BLL.JWT;
using BAHelper.BLL.Services;
using BAHelper.Common.Auth;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BAHelper.BLL.Services.Backgound;

namespace BAHelper.API.Extensions
{
    public static class ServiceExtentions
    {
        public static void RegisterCustomServices (this IServiceCollection services)
        {
            services.AddScoped<DocumentService>();
            services.AddScoped<UserService>();
            services.AddScoped<WordService>();
            services.AddSingleton<IFileProvider>(
                            new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(),
                                "./LocalFiles")));
            services.AddScoped<DownloadService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<ProjectTaskService>();
            services.AddScoped<AuthService>();
            services.AddScoped<JwtFactory>();
            services.AddScoped<MailService>();
            services.AddScoped<ClasterizationService>();
            //services.AddHostedService<ScheduledService>();
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps("BAHelper.BLL");
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["SecretJWTKey"]; // get value from system environment
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var validFor = Convert.ToInt64(configuration["ExpireTokenTimeInMin"]);

            // Get options from app settings
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.ValidForInMin = TimeSpan.FromMinutes(validFor);
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = true,
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

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        }
    }
}
