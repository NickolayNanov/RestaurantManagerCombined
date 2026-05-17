using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.GetByMonth
{
    internal class GetRestaurantMonthlyReportByMonthHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetRestaurantMonthlyReportByMonthQuery, RestaurantMonthlyReportResponse>
    {
        public async Task<RestaurantMonthlyReportResponse> Handle(GetRestaurantMonthlyReportByMonthQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.RestaurantMonthlyReports
                .AsNoTracking()
                .Where(x =>
                    x.RestaurantId == request.RestaurantId &&
                    x.Year == request.Year &&
                    x.Month == request.Month &&
                    x.Restaurant.OwnerId == currentUserService.UserId)
                .ProjectTo<RestaurantMonthlyReportResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(RestaurantMonthlyReport), "Monthly report was not found.");
        }
    }
}
