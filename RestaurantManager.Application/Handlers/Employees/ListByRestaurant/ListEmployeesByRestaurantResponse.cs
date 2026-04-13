using RestaurantManager.Application.Handlers.Employees.GetById;

namespace RestaurantManager.Application.Handlers.Employees.ListByRestaurant
{
    public record ListEmployeesByRestaurantResponse(IEnumerable<GetEmployeeByIdResponse> Employees);
}
