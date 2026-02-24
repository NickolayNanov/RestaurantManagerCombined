namespace RestaurantManager.Domain.Entities
{
    public class MenuItemCategory : EntityBase
    {
        public string MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
