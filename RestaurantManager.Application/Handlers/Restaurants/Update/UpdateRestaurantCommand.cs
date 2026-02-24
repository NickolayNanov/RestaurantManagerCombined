using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.Update
{
    public record UpdateRestaurantCommand : IRequest<UpdateRestaurantResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImgUrl { get; set; }

        // public virtual IEnumerable<Menu> Menus { get; set; } = new HashSet<Menu>();

        public Guid? OwnerId { get; set; }
    }
}
