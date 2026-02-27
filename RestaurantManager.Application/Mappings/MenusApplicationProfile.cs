using AutoMapper;
using RestaurantManager.Application.Handlers.Menus.Create;
using RestaurantManager.Application.Handlers.Menus.GetById;
using RestaurantManager.Application.Handlers.Menus.Update;
using RestaurantManager.Application.Handlers.Restaurants.GetById;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class MenusApplicationProfile : Profile
    {
        public MenusApplicationProfile()
        {
            // get
            this.CreateMap<Menu, GetMenuByIdResponse>()
                .ForMember(x => x.Items, y => y.MapFrom(z => z.MenuItems));
            this.CreateMap<Menu, RestaurantMenus>();


            // create
            this.CreateMap<CreateMenuCommand, Menu>();
            this.CreateMap<Menu, CreateMenuResponse>();

            // update
            this.CreateMap<UpdateMenuCommand, Menu>();
            this.CreateMap<Menu, UpdateMenuResponse>();
        }
    }
}
