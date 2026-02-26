using RestaurantManager.Application.Handlers.Categories.GetById;

namespace RestaurantManager.Application.Handlers.Categories.GetMany
{
    public record GetManyCategoriesResponse(IEnumerable<GetCategoryByIdResponse> Categories);
}
