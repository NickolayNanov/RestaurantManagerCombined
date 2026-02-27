using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.GetById
{
    public record GetRestaurantByIdResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Cuisine { get; set; }

        public OpenClosed Status { get; set; }

        public string ImgUrl { get; set; }

        public string OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public IEnumerable<RestaurantMenus> Menus { get; set; }
    }

    public record RestaurantMenus
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
