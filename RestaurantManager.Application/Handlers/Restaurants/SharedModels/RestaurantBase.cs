using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.SharedModels
{
    public record RestaurantBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Cuisine { get; set; }

        public OpenClosed Status { get; set; }

        public string ImgUrl { get; set; }

        public string OwnerId { get; set; }
    }
}
