using AutoMapper;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class RestaurantsApplicationProfile : Profile
    {
        public RestaurantsApplicationProfile()
        {
            // create
            this.CreateMap<CreateRestaurantCommand, Restaurant>();
            this.CreateMap<Restaurant, CreateRestaurantResponse>();
        }
    }
}
