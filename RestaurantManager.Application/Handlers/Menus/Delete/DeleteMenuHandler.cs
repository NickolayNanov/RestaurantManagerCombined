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
            var menu = await restaurantManagerDbContext.Menus.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (menu is null)
            {
                logger.LogWarning("Menu with id {Id} not found", request.Id);
                throw new ResourceNotFoundException(nameof(Menu), $"Menu with id {request.Id} was not found when trying to delete.");
            }

            restaurantManagerDbContext.Menus.Remove(menu);

            return new DeleteMenuResponse(true);
        }
    }
}
