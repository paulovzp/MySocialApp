using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MySocialApp.Api.AuthOverrides;
using MySocialApp.Domain;
using MySocialApp.Persistence;
using System.Text;

namespace MySocialApp.Api.Extensions;

public static class AuthenticationExtension
{

    public static IServiceCollection AddMySocialAppAuthentication(this IServiceCollection services)
    {


        services.AddIdentityCore<User>(opt =>
        {
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = true;
            opt.Password.RequireDigit = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;


            opt.User.RequireUniqueEmail = true;

            opt.SignIn.RequireConfirmedEmail = false;
            opt.SignIn.RequireConfirmedAccount = false;
            opt.SignIn.RequireConfirmedPhoneNumber = false;
        })
        .AddApiEndpoints()
        .AddEntityFrameworkStores<MySocialAppContext>()
        .AddDefaultTokenProviders();

        services.AddAuthorization();

        services.AddAuthentication(IdentityConstants.BearerScheme)
        .AddCookie(IdentityConstants.ApplicationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "mysocialapp",
                ValidAudience = "mysocialapp",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Xjl2ZGJjUkVvaHB4WmFNaWt6WjRQZ1lxNHJ0N0FuRlY="))
            };
        })
        .AddBearerToken(IdentityConstants.BearerScheme);


        return services;
    }

    public static WebApplication UseMySocialAppAuthentication(this WebApplication app)
    {
        //app.MapIdentityApi<User>();
        app.MapIdentityApiFilterable<User>(new IdentityApiEndpointRouteBuilderOptions()
        {
            ExcludeRegisterPost = false,
            ExcludeLoginPost = false,
            ExcludeRefreshPost = false,
            ExcludeConfirmEmailGet = true,
            ExcludeResendConfirmationEmailPost = true,
            ExcludeForgotPasswordPost = true,
            ExcludeResetPasswordPost = true,
            // setting ExcludeManageGroup to false will disable
            // 2FA and both Info Actions
            ExcludeManageGroup = true,
            Exclude2faPost = true,
            ExcludegInfoGet = true,
            ExcludeInfoPost = true,
        });

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
