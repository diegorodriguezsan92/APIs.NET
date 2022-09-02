using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniversityAPIrestfull.Models.DataModels;

namespace UniversityAPIrestfull
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices (this IServiceCollection Services, IConfiguration Configuration)
        {
            // Add JWT Settings
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

            // Add Singleton of WJT Settings
            Services.AddSingleton(bindJwtSettings);

            // Add Authentication
            Services.
                AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Authenticates users.
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    // Checks the users authentication.

                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),  /////////////// hasta aquí // 2º hasta aquí
                        ValidateIssuer = bindJwtSettings.ValidateIssuer,
                        ValidIssuer = bindJwtSettings.ValidIssuer,
                        ValidateAudience = bindJwtSettings.ValidateAudience,
                        ValidAudience = bindJwtSettings.ValidAudience,
                        RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                        ValidateLifetime = bindJwtSettings.ValidateLifetime,
                        ClockSkew = TimeSpan.FromDays(1)
                    };
                });
        }
    }
}
