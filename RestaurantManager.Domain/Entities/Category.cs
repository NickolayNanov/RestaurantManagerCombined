namespace RestaurantManager.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<MenuItemCategory> MenuItemCategories { get; set; } = new HashSet<MenuItemCategory>();
    }
}
