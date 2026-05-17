using MediatR;

namespace RestaurantManager.Application.Handlers.Dashboard.PerformanceAnalytics
{
    public record GetPerformanceAnalyticsQuery(int Months) : IRequest<GetPerformanceAnalyticsResponse>;
}
