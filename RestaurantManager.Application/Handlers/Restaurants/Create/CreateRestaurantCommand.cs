using MediatR;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.Create
{
    public record CreateRestaurantCommand : IRequest<CreateRestaurantResponse>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string Cuisine { get; set; }

        public OpenClosed Status { get; set; }

        public string ImgUrl { get; set; }
    }
}
