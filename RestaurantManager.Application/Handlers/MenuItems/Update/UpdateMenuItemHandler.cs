using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Handlers.Menus.Create;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.MenuItems.Update
{
    internal class UpdateMenuItemHandler(
        IRestaurantManagerDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateMenuHandler> logger) : IRequestHandler<UpdateMenuItemCommand, UpdateMenuItemResponse>
    {
        public async Task<UpdateMenuItemResponse> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await dbContext.MenuItems.Include(mi => mi.Category).AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(MenuItem), $"Menu Item with id {request.Id} was not found when trying to update.");

            var entity = mapper.Map<MenuItem>(request);

            entity.MenuId = menuItem.MenuId;
            entity.CreatedBy = menuItem.CreatedBy;
            entity.CreatedAt = menuItem.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = currentUserService.UserId;

            dbContext.MenuItems.Update(entity);

            logger.LogInformation($"Updated menu with id {entity.Id}.");
            var response = mapper.Map<UpdateMenuItemResponse>(entity);
            response.CategoryText = menuItem.Category.Name;

            return response;
        }
    }
}
