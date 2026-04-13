using MediatR;

namespace RestaurantManager.Application.Handlers.Employees.ListByRestaurant
{
    public record ListEmployeesByRestaurantQuery(Guid RestaurantId) : IRequest<ListEmployeesByRestaurantResponse>;
}
