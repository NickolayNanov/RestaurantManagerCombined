using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Menus.Delete
{
    internal class DeleteMenuHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<DeleteMenuCommand, DeleteMenuResponse>
    {
        public async Task<DeleteMenuResponse> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await restaurantManagerDbContext.Menus.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (menuItem is null)
            {
                logger.LogWarning("Menu Item with id {Id} not found", request.Id);
                throw new ResourceNotFoundException(nameof(MenuItem), $"Menu Item with id {request.Id} was not found when trying to delete.");
            }

            restaurantManagerDbContext.Menus.Remove(menuItem);

            return new DeleteMenuResponse(true);
        }
    }
}
