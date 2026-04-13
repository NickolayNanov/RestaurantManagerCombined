using MediatR;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Employees.Delete
{
    internal class DeleteEmployeeHandler(
        ILogger<DeleteEmployeeHandler> logger,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
    {
        public async Task<DeleteEmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existing = await dbContext.Employees.FindAsync(request.Id, cancellationToken);

            if (existing is null)
            {
                logger.LogError($"Could not find employee with id to delete: {request.Id}");
                throw new ResourceNotFoundException(nameof(Employee), $"Could not find employee with id: {request.Id}");
            }

            dbContext.Employees.Remove(existing);

            return new DeleteEmployeeResponse(true);
        }
    }
}
