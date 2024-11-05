using Microsoft.OpenApi.Models;

namespace MySocialApp.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddMySocialAppSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "MySocialApp", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                                    "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                                    "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                                    "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            });

            return services;
        }

        public static WebApplication UseMySocialAppSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MySocialApp v1"));
            }

            return app;
        }
    }
}
