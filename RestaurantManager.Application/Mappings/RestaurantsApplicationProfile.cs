using AutoMapper;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Application.Handlers.Restaurants.GetById;
using RestaurantManager.Application.Handlers.Restaurants.Update;
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

            // update
            this.CreateMap<UpdateRestaurantCommand, Restaurant>();
            this.CreateMap<Restaurant, UpdateRestaurantResponse>();

            // gets
            this.CreateMap<Restaurant, GetRestaurantByIdResponse>();
        }
    }
}
