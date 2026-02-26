using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Menus.Shared
{
    public record MenuBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public bool IsActive { get; set; }

        public MenuType Type { get; set; }
    }
}
