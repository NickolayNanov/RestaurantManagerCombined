using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Handlers.Menus.Create;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Menus.Update
{
    internal class UpdateMenuHandler(
        IRestaurantManagerDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateMenuHandler> logger) : IRequestHandler<UpdateMenuCommand, UpdateMenuResponse>
    {
        public async Task<UpdateMenuResponse> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await dbContext.Menus.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(Menu), $"Menu with id {request.Id} was not found when trying to update.");

            var entity = mapper.Map<Menu>(request);

            entity.CreatedBy = menu.CreatedBy;
            entity.CreatedAt = menu.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = currentUserService.UserId;

            dbContext.Menus.Update(entity);

            logger.LogInformation($"Updated menu with id {entity.Id}.");
            var response = mapper.Map<UpdateMenuResponse>(entity);

            return response;
        }
    }
}
