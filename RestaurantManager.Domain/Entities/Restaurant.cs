namespace RestaurantManager.Domain.Entities
{
    public class Restaurant : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        public virtual IEnumerable<Menu> Menus { get; set; } = new HashSet<Menu>();

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}
