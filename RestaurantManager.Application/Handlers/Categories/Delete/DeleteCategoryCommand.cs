using MediatR;

namespace RestaurantManager.Application.Handlers.Categories.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<DeleteCategoryResponse>;
}
