using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.ListByRestaurant
{
    internal class ListRestaurantMonthlyReportsByRestaurantHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<ListRestaurantMonthlyReportsByRestaurantQuery, ListRestaurantMonthlyReportsByRestaurantResponse>
    {
        public async Task<ListRestaurantMonthlyReportsByRestaurantResponse> Handle(ListRestaurantMonthlyReportsByRestaurantQuery request, CancellationToken cancellationToken)
        {
            var restaurantExists = await dbContext.Restaurants
                .AnyAsync(x => x.Id == request.RestaurantId && x.OwnerId == currentUserService.UserId, cancellationToken);

            if (!restaurantExists)
            {
                throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id: {request.RestaurantId} was not found.");
            }

            var reports = await dbContext.RestaurantMonthlyReports
                .AsNoTracking()
                .Where(x => x.RestaurantId == request.RestaurantId)
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ProjectTo<RestaurantMonthlyReportResponse>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ListRestaurantMonthlyReportsByRestaurantResponse(reports);
        }
    }
}
