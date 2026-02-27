using RestaurantManager.Application.Handlers.Menus.GetById;
using RestaurantManager.Application.Handlers.Restaurants.SharedModels;

namespace RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo
{
    public record GetRestaurantInfoResponse : RestaurantBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public IEnumerable<GetMenuByIdResponse> Menus { get; set; }
    }
}
