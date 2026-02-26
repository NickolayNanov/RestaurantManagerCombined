using MediatR;

namespace RestaurantManager.Application.Handlers.Menus.GetById
{
    public record GetMenuByIdQuery(Guid Id) : IRequest<GetMenuByIdResponse>;
}
