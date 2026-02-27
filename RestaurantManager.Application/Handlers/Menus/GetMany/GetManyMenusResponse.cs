using RestaurantManager.Application.Handlers.Menus.GetById;

namespace RestaurantManager.Application.Handlers.Menus.GetMany
{
    public record GetManyMenusResponse(IEnumerable<GetMenuByIdResponse> Menus);
}
