namespace RestaurantManager.Domain.Entities
{
    public class MenuItem : EntityBase
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public bool IsActive { get; set; }

        public string MenuId { get; set; }

        public Menu Menu { get; set; }

        public virtual IEnumerable<MenuItemCategory> MenuItemCategories { get; set; } = new HashSet<MenuItemCategory>();
    }
}
