using MediatR;
using RestaurantManager.Application.Handlers.MenuItems.Shared;

namespace RestaurantManager.Application.Handlers.MenuItems.Create
{
    public record CreateMenuItemCommand : MenuItemBase, IRequest<CreateMenuItemResponse>
    {
        public Guid? MenuId { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
