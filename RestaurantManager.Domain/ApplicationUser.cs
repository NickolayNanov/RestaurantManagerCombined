using Microsoft.AspNetCore.Identity;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
    }
}
