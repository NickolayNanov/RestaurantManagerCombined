using MediatR;

namespace RestaurantManager.Application.Handlers.Employees.GetById
{
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<GetEmployeeByIdResponse>;
}
