using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Employees.Update
{
    internal class UpdateEmployeeHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IImageUploadService imageUploadService,
        ILogger<UpdateEmployeeHandler> logger,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponse>
    {
        public async Task<UpdateEmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee is null)
            {
                logger.LogError($"Could not find employee with id to update: {request.Id}");
                throw new ResourceNotFoundException(nameof(Employee), $"Could not find employee with id: {request.Id}");
            }

            var employeeEntity = mapper.Map<Employee>(request);

            employeeEntity.RestaurantId = employee.RestaurantId;
            employeeEntity.CreatedAt = employee.CreatedAt;
            employeeEntity.CreatedBy = employee.CreatedBy;
            employeeEntity.ImgUrl = request.Image is null
                ? employee.ImgUrl
                : await imageUploadService.UploadAsync(request.Image, ImageUploadFolders.Employees, cancellationToken);
            employeeEntity.UpdatedBy = currentUserService.UserId;
            employeeEntity.UpdatedAt = DateTime.UtcNow;

            dbContext.Employees.Update(employeeEntity);
            logger.LogInformation($"Updated employee with id: {request.Id}");

            var response = mapper.Map<UpdateEmployeeResponse>(employeeEntity);

            return response;
        }
    }
}
