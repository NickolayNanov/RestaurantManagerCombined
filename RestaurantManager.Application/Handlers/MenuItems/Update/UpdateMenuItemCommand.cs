using MediatR;
using RestaurantManager.Application.Handlers.MenuItems.Shared;

namespace RestaurantManager.Application.Handlers.MenuItems.Update
{
    public record UpdateMenuItemCommand : MenuItemBase, IRequest<UpdateMenuItemResponse>
    {
        public Guid? Id { get; set; }
    }
}
