using RestaurantManager.Application.Handlers.MenuItems.GetById;

namespace RestaurantManager.Application.Handlers.MenuItems.GetByMenu
{
    public record GetMenuItemsByMenuResponse(IEnumerable<GetMenuItemByIdResponse> MenuItems);
}
