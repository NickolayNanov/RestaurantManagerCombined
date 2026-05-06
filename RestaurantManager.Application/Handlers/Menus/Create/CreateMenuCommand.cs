using MediatR;
using RestaurantManager.Application.Handlers.Menus.Shared;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Handlers.Menus.Create
{
    public record CreateMenuCommand : MenuBase, IRequest<CreateMenuResponse>
    {
        public UploadFile Image { get; set; }
    }
}
