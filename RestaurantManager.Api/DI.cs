using Microsoft.OpenApi;
using RestaurantManager.Api.Mappings;
using RestaurantManager.Api.Middlewares;
using RestaurantManager.Infrastructure;
using System;

namespace RestaurantManager.Api
{
    public static class DI
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = ctx =>
                {
                    ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
                };
            });

            services.AddExceptionHandler<GlobalExceptionHandler>();

            services.AddScoped<EfCoreTransactionMiddleware>();

            services.AddControllers();
            services.AddOpenApi();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Restaurant Manager API", Version = "v1" });

                // Add JWT Bearer definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {your JWT token}"
                });
            });

            services.AddHttpContextAccessor();

            // automapper
            services.AddAutoMapper(typeof(RestaurantsPresentationProfile).Assembly);

            return services;
        }

        // start up configuration of the middlewares pipeline
        public static WebApplication BuildMiddlewaresPipeline(this WebApplication app)
        {
            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.MapFallbackToFile("index.html");

            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseIdentityAndRoles();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<EfCoreTransactionMiddleware>();

            app.MigrateDatabase(true);

            app.MapControllers();

            return app;
        }
    }
}
