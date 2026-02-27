using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Menus.Create
{
    internal class CreateMenuHandler(
        IRestaurantManagerDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateMenuHandler> logger) : IRequestHandler<CreateMenuCommand, CreateMenuResponse>
    {
        public async Task<CreateMenuResponse> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await dbContext.Restaurants.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.RestaurantId, cancellationToken);

            if (restaurant is null)
            {
                logger.LogWarning($"Restaurant with id {request.RestaurantId} not found.");
                throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id {request.RestaurantId} was not found.");
            }

            var entity = mapper.Map<Menu>(request);

            entity.CreatedBy = currentUserService.UserId;
            entity.CreatedAt = DateTime.UtcNow;

            dbContext.Menus.Add(entity);

            logger.LogInformation($"Created menu with id {entity.Id}.");
            var response = mapper.Map<CreateMenuResponse>(entity);

            return response;
        }
    }
}
