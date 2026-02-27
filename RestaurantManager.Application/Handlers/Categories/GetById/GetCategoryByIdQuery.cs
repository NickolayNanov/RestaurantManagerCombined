using MediatR;

namespace RestaurantManager.Application.Handlers.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<GetCategoryByIdResponse>;
}
