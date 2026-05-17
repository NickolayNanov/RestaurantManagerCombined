using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using System.Globalization;

namespace RestaurantManager.Application.Handlers.Dashboard.PerformanceAnalytics
{
    internal class GetPerformanceAnalyticsHandler(
        ICurrentUserService currentUserService,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetPerformanceAnalyticsQuery, GetPerformanceAnalyticsResponse>
    {
        public async Task<GetPerformanceAnalyticsResponse> Handle(GetPerformanceAnalyticsQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow;
            var firstMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-(request.Months - 1));

            var monthBuckets = Enumerable.Range(0, request.Months)
                .Select(offset => firstMonth.AddMonths(offset))
                .Select(date => new { date.Year, date.Month, Label = date.ToString("MMM", CultureInfo.InvariantCulture) })
                .ToList();

            var startKey = firstMonth.Year * 100 + firstMonth.Month;
            var endKey = today.Year * 100 + today.Month;

            var aggregates = await dbContext.RestaurantMonthlyReports
                .AsNoTracking()
                .Where(x =>
                    x.Restaurant.OwnerId == currentUserService.UserId &&
                    (x.Year * 100 + x.Month) >= startKey &&
                    (x.Year * 100 + x.Month) <= endKey)
                .GroupBy(x => new { x.Year, x.Month })
                .Select(group => new
                {
                    group.Key.Year,
                    group.Key.Month,
                    Revenue = group.Sum(x => x.Revenue),
                    Rating = group.Average(x => x.Rating)
                })
                .ToListAsync(cancellationToken);

            var response = monthBuckets.Select(month =>
            {
                var aggregate = aggregates.FirstOrDefault(x => x.Year == month.Year && x.Month == month.Month);
                return new PerformanceAnalyticsMonthResponse(
                    month.Year,
                    month.Month,
                    month.Label,
                    aggregate?.Revenue ?? 0,
                    aggregate?.Rating);
            });

            return new GetPerformanceAnalyticsResponse(response);
        }
    }
}
