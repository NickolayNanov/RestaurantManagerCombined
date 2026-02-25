namespace RestaurantManager.Domain.Entities
{
    public class MenuItemCategory : AuditableEntity
    {
        public Guid MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
