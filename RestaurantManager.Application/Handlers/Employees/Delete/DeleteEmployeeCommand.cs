using MediatR;

namespace RestaurantManager.Application.Handlers.Employees.Delete
{
    public record DeleteEmployeeCommand(Guid Id) : IRequest<DeleteEmployeeResponse>;
}
