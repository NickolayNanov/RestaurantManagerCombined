using MediatR;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.ListByRestaurant
{
    public record ListRestaurantMonthlyReportsByRestaurantQuery(Guid RestaurantId) : IRequest<ListRestaurantMonthlyReportsByRestaurantResponse>;
}
