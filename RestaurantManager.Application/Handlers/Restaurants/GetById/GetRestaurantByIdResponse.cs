using RestaurantManager.Application.Handlers.Menus.GetById;
using RestaurantManager.Application.Handlers.Restaurants.SharedModels;

namespace RestaurantManager.Application.Handlers.Restaurants.GetById
{
    public record GetRestaurantByIdResponse : RestaurantBase
    {
        public Guid Id { get; set; }

        public IEnumerable<GetMenuByIdResponse> Menus { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
