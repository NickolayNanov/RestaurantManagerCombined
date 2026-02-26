using RestaurantManager.Application.Handlers.MenuItems.GetById;

namespace RestaurantManager.Application.Handlers.MenuItems.GetMany
{
    public record GetManyMenuItemsResponse(IEnumerable<GetMenuItemByIdResponse> MenuItems);
}
