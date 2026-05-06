using MediatR;
using RestaurantManager.Application.Handlers.MenuItems.Shared;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Handlers.MenuItems.Update
{
    public record UpdateMenuItemCommand : MenuItemBase, IRequest<UpdateMenuItemResponse>
    {
        public Guid? Id { get; set; }

        public Guid? CategoryId { get; set; }

        public UploadFile Image { get; set; }
    }
}
