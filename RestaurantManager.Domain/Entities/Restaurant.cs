namespace RestaurantManager.Domain.Entities
{
    public class Restaurant : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Cuisine { get; set; }

        public OpenClosed Status { get; set; }

        public string ImgUrl { get; set; }

        public virtual IEnumerable<Menu> Menus { get; set; } = new HashSet<Menu>();

        public virtual IEnumerable<Employee> Employees { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}
