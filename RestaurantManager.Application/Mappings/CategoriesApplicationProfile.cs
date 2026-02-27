using AutoMapper;
using RestaurantManager.Application.Handlers.Categories.Create;
using RestaurantManager.Application.Handlers.Categories.GetById;
using RestaurantManager.Application.Handlers.Categories.Update;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class CategoriesApplicationProfile : Profile
    {
        public CategoriesApplicationProfile()
        {
            this.CreateMap<Category, GetCategoryByIdResponse>()
                .ForMember(x => x.MenuItemsCount, y => y.MapFrom(z => z.MenuItems.Count()));

            this.CreateMap<CreateCategoryCommand, Category>();
            this.CreateMap<Category, CreateCategoryResponse>();

            this.CreateMap<UpdateCategoryCommand, Category>();
            this.CreateMap<Category, UpdateCategoryResponse>();
        }
    }
}
