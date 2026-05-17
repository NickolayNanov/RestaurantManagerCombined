using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Create
{
    internal class CreateRestaurantMonthlyReportHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateRestaurantMonthlyReportHandler> logger,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<CreateRestaurantMonthlyReportCommand, RestaurantMonthlyReportResponse>
    {
        public async Task<RestaurantMonthlyReportResponse> Handle(CreateRestaurantMonthlyReportCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await dbContext.Restaurants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.RestaurantId && x.OwnerId == currentUserService.UserId, cancellationToken);

            if (restaurant is null)
            {
                throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id: {request.RestaurantId} was not found.");
            }

            var exists = await dbContext.RestaurantMonthlyReports
                .AnyAsync(x => x.RestaurantId == request.RestaurantId && x.Year == request.Year && x.Month == request.Month, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException("A monthly report already exists for this restaurant and month.");
            }

            var report = mapper.Map<RestaurantMonthlyReport>(request);
            report.RestaurantId = request.RestaurantId!.Value;
            report.CreatedAt = DateTime.UtcNow;
            report.CreatedBy = currentUserService.UserId;

            await dbContext.RestaurantMonthlyReports.AddAsync(report, cancellationToken);
            logger.LogInformation("Created monthly report for restaurant {RestaurantId}, {Year}-{Month}", report.RestaurantId, report.Year, report.Month);

            return mapper.Map<RestaurantMonthlyReportResponse>(report);
        }
    }
}
