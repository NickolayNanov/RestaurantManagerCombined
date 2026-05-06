using MediatR;
using RestaurantManager.Application.Handlers.MenuItems.Shared;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Handlers.MenuItems.Create
{
    public record CreateMenuItemCommand : MenuItemBase, IRequest<CreateMenuItemResponse>
    {
        public Guid? MenuId { get; set; }

        public Guid? CategoryId { get; set; }

        public UploadFile Image { get; set; }
    }
}
