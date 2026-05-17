using Microsoft.EntityFrameworkCore;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Domain
{
    public interface IRestaurantManagerDbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProfileDetails> ProfileDetails { get; set; }

        Task<int> SaveChangesAsync(CancellationToken ct = default);

        int SaveChanges();
    }
}
