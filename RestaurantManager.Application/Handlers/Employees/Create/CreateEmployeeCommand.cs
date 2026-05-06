using MediatR;
using RestaurantManager.Application.Handlers.Employees.Shared;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Handlers.Employees.Create
{
    public record CreateEmployeeCommand : EmployeeBase, IRequest<CreateEmployeeResponse>
    {
        public Guid? RestaurantId { get; set; }

        public UploadFile Image { get; set; }
    }
}
