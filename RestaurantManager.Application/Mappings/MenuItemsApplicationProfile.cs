using AutoMapper;
using RestaurantManager.Application.Handlers.MenuItems.Create;
using RestaurantManager.Application.Handlers.MenuItems.GetById;
using RestaurantManager.Application.Handlers.MenuItems.Update;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class MenuItemsApplicationProfile : Profile
    {
        public MenuItemsApplicationProfile()
        {
            // get
            this.CreateMap<MenuItem, GetMenuItemByIdResponse>()
                .ForMember(d => d.CategoryText, opt => opt.MapFrom(s => s.Category.Name));

            // create
            this.CreateMap<CreateMenuItemCommand, MenuItem>();
            this.CreateMap<MenuItem, CreateMenuItemResponse>();

            // update
            this.CreateMap<UpdateMenuItemCommand, MenuItem>();
            this.CreateMap<MenuItem, UpdateMenuItemResponse>();
        }
    }
}
