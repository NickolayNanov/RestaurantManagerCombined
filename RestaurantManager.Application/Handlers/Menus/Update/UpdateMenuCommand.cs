using MediatR;
using RestaurantManager.Application.Handlers.Menus.Shared;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Handlers.Menus.Update
{
    public record UpdateMenuCommand : MenuBase, IRequest<UpdateMenuResponse>
    {
        public Guid? Id { get; set; }

        public UploadFile Image { get; set; }
    }
}
