using MediatR;

namespace RestaurantManager.Application.Handlers.Categories.GetMany
{
    public record ListAllCategoriesQuery : IRequest<GetManyCategoriesResponse>;
}
