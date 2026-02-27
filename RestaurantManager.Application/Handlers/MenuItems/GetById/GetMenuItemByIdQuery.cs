using MediatR;

namespace RestaurantManager.Application.Handlers.MenuItems.GetById
{
    public record GetMenuItemByIdQuery(Guid Id) : IRequest<GetMenuItemByIdResponse>;
}
