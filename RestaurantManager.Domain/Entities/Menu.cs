namespace RestaurantManager.Domain.Entities
{
    public class Menu : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public bool IsActive { get; set; }

        public MenuType Type { get; set; }

        public virtual IEnumerable<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();

        public Guid RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
