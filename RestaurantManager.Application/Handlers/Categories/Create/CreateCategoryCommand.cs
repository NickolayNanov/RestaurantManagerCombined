using MediatR;
using RestaurantManager.Application.Handlers.Categories.Shared;

namespace RestaurantManager.Application.Handlers.Categories.Create
{
    public record CreateCategoryCommand : CategoryBase, IRequest<CreateCategoryResponse>;
}
