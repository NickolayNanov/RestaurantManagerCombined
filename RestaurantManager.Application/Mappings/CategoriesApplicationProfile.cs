using AutoMapper;
using RestaurantManager.Application.Handlers.Categories.GetById;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class CategoriesApplicationProfile : Profile
    {
        public CategoriesApplicationProfile()
        {
            this.CreateMap<Category, GetCategoryByIdResponse>();
        }
    }
}
