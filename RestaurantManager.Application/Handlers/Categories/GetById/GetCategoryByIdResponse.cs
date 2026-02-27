using RestaurantManager.Application.Handlers.Categories.Shared;

namespace RestaurantManager.Application.Handlers.Categories.GetById
{
    public record GetCategoryByIdResponse : CategoryBase
    {
        public Guid Id { get; set; }
    }
}
