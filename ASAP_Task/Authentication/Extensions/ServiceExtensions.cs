using System.Text;
using ASAP_Task.Controllers;
using ASAP_Task.Infrastructure.Helper;
using ASAP_Task.WebAPI.Authentication.Context;
using ASAP_Task.WebAPI.Authentication.Entity;
using ASAP_Task.WebAPI.Controllers.Identity;
using ASAP_Task.WepApi.Authentication.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ASAP_Task.WepApi.Authentication.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["JWTConfig:Secret_Key"]);
            var tokenValidationParameter = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true, // for refresh token 
                ValidateLifetime = false,

                //ValidIssuer = configuration["JWTConfig:ValidIssuer"],
                //ValidAudience = configuration["JWTConfig:ValidAudience"],
            };
            services.AddScoped<UserApplicationManager>();
            services.AddScoped<AuthenticationController>();
            services.AddScoped<UserController>();
            services.AddScoped<UserContext>();

            services.AddSingleton(tokenValidationParameter);
            var connectionString = configuration.GetConnectionString("InstallDirctDataConnectionString");
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<AuthContext>(opt => opt.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true; // after authorization do u wanna save this token inside the authentication Property
                jwt.TokenValidationParameters = tokenValidationParameter;
            });

        }
    }
}
