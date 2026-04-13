using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;
using RestaurantManager.Infrastructure.EF.EntityConfigurations;

namespace RestaurantManager.Infrastructure
{
    public class RestaurantManagerDbContext : IdentityDbContext<ApplicationUser>, IRestaurantManagerDbContext
    {
        public RestaurantManagerDbContext(DbContextOptions<RestaurantManagerDbContext> options) : base(options) { }

        public virtual DbSet<Restaurant> Restaurants { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MenuEntityConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
