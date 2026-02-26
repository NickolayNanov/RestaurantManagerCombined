using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Handlers.Menus.Delete;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.MenuItems.Delete
{
    internal class DeleteMenuItemHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<DeleteMenuItemCommand, DeleteMenuItemResponse>
    {
        public async Task<DeleteMenuItemResponse> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await restaurantManagerDbContext.MenuItems.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (menuItem is null)
            {
                logger.LogWarning("Menu with id {Id} not found", request.Id);
                throw new ResourceNotFoundException(nameof(Menu), $"Menu with id {request.Id} was not found when trying to delete.");
            }

            restaurantManagerDbContext.MenuItems.Remove(menuItem);

            return new DeleteMenuItemResponse(true);
        }
    }
}
