using MediatR;
using RestaurantManager.Application.Handlers.Employees.Shared;

namespace RestaurantManager.Application.Handlers.Employees.Create
{
    public record CreateEmployeeCommand : EmployeeBase, IRequest<CreateEmployeeResponse>
    {
        public Guid? RestaurantId { get; set; }
    }
}
