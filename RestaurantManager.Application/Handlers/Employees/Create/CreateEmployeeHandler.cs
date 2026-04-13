using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Employees.Create
{
    internal class CreateEmployeeHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateEmployeeHandler> logger,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await dbContext.Restaurants.FindAsync(request.RestaurantId, cancellationToken);

            if (restaurant is null)
            {
                logger.LogError($"Could not find restaurant with id: {request.RestaurantId}");
                throw new ResourceNotFoundException(nameof(Restaurant), $"Could not find restaurant with id: {request.RestaurantId}");
            }

            var employeeEntity = mapper.Map<Employee>(request);

            employeeEntity.CreatedAt = DateTime.UtcNow;
            employeeEntity.CreatedBy = currentUserService.UserId;

            await dbContext.Employees.AddAsync(employeeEntity);
            logger.LogInformation($"Created employee with id: {employeeEntity.Id}");

            var response = mapper.Map<CreateEmployeeResponse>(employeeEntity);

            return response;
        }
    }
}
