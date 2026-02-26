using RestaurantManager.Application.Handlers.Menus.Shared;

namespace RestaurantManager.Application.Handlers.Menus.GetById
{
    public record GetMenuByIdResponse : MenuBase
    {
        public Guid Id { get; set; }

        public IEnumerable<MenuItemRecord> Items { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }

    public record MenuItemRecord
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public bool IsActive { get; set; }
    }
}
