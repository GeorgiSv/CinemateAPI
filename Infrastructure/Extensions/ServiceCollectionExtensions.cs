namespace CinemateAPI.Infrastructure.Extensions
{
    using System.Text;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection serviceProvider, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            serviceProvider
                  .AddAuthentication(x =>
                  {
                      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  })
                  .AddJwtBearer(x =>
                  {
                      x.RequireHttpsMetadata = false;
                      x.SaveToken = true;
                      x.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = false,
                          IssuerSigningKey = new SymmetricSecurityKey(key),
                          ValidateIssuer = false,
                          ValidateAudience = false
                      };
                  });
        }
    }
}
