using MediatR;

namespace RestaurantManager.Application.Handlers.MenuItems.GetByMenu
{
    public record GetMenuItemsByMenuQuery(Guid MenuId) : IRequest<GetMenuItemsByMenuResponse>;
}
