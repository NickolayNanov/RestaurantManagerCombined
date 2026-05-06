namespace RestaurantManager.Application.Handlers.MenuItems.Shared
{
    public record MenuItemBase
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
