using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.GetById
{
    internal class GetRestaurantByIdHandler(
        ILogger<GetRestaurantByIdHandler> logger,
        IMapper mapper,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetRestaurantByIdQuery, GetRestaurantByIdResponse>
    {
        public async Task<GetRestaurantByIdResponse> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await dbContext.Restaurants
                .AsSplitQuery()
                .AsNoTracking()
                .ProjectTo<GetRestaurantByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            return restaurant;
        }
    }
}
