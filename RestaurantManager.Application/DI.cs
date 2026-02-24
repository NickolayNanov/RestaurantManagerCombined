using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Application.Mappings;
using RestaurantManager.Application.MediatR;
using RestaurantManager.Application.Services;
using RestaurantManager.Application.Services.Interfaces;

namespace RestaurantManager.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // automapper
            services.AddAutoMapper(typeof(RestaurantsApplicationProfile).Assembly);

            // fluent validation
            services.AddValidatorsFromAssemblyContaining<CreateRestaurantCommandValidator>();

            // MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CurrentUserService>());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddCustomServices();

            return services;
        }

        private static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
