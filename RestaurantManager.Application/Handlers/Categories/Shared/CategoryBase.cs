namespace RestaurantManager.Application.Handlers.Categories.Shared
{
    public record CategoryBase
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
