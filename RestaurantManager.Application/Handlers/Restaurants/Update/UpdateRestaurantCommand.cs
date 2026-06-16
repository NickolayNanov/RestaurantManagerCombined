using MediatR;
using RestaurantManager.Application.Services.Models;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.Update
{
    public record UpdateRestaurantCommand : IRequest<UpdateRestaurantResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Cuisine { get; set; }

        public OpenClosed Status { get; set; }

        public UploadFile Image { get; set; }

        // public virtual IEnumerable<Menu> Menus { get; set; } = new HashSet<Menu>();

        public Guid? OwnerId { get; set; }
    }
}
