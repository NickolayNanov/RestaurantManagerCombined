using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Update
{
    internal class UpdateRestaurantMonthlyReportHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<UpdateRestaurantMonthlyReportHandler> logger,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<UpdateRestaurantMonthlyReportCommand, RestaurantMonthlyReportResponse>
    {
        public async Task<RestaurantMonthlyReportResponse> Handle(UpdateRestaurantMonthlyReportCommand request, CancellationToken cancellationToken)
        {
            var report = await dbContext.RestaurantMonthlyReports
                .Include(x => x.Restaurant)
                .FirstOrDefaultAsync(x =>
                    x.RestaurantId == request.RestaurantId &&
                    x.Year == request.Year &&
                    x.Month == request.Month &&
                    x.Restaurant.OwnerId == currentUserService.UserId,
                    cancellationToken);

            if (report is null)
            {
                throw new ResourceNotFoundException(nameof(RestaurantMonthlyReport), "Monthly report was not found.");
            }

            report.Revenue = request.Revenue;
            report.Rating = request.Rating;
            report.UpdatedAt = DateTime.UtcNow;
            report.UpdatedBy = currentUserService.UserId;

            logger.LogInformation("Updated monthly report for restaurant {RestaurantId}, {Year}-{Month}", report.RestaurantId, report.Year, report.Month);

            return mapper.Map<RestaurantMonthlyReportResponse>(report);
        }
    }
}
