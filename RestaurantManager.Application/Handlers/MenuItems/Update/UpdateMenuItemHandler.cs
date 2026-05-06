using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Handlers.Menus.Create;
using RestaurantManager.Application.Services;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.MenuItems.Update
{
    internal class UpdateMenuItemHandler(
        IRestaurantManagerDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateMenuHandler> logger,
        IImageUploadService imageUploadService) : IRequestHandler<UpdateMenuItemCommand, UpdateMenuItemResponse>
    {
        public async Task<UpdateMenuItemResponse> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await dbContext.MenuItems.Include(mi => mi.Category).FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(MenuItem), $"Menu Item with id {request.Id} was not found when trying to update.");

            var category = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(Category), $"Category with id {request.CategoryId} not found");

            menuItem.Name = request.Name;
            menuItem.Price = request.Price;
            menuItem.IsActive = request.IsActive;
            menuItem.CategoryId = request.CategoryId!.Value;
            menuItem.UpdatedAt = DateTime.UtcNow;
            menuItem.UpdatedBy = currentUserService.UserId;

            if (request.Image is not null)
            {
                menuItem.ImgUrl = await imageUploadService.UploadAsync(
                    request.Image,
                    ImageUploadFolders.MenuItems,
                    cancellationToken);
            }

            dbContext.MenuItems.Update(menuItem);

            logger.LogInformation($"Updated menu item with id {menuItem.Id}.");
            var response = mapper.Map<UpdateMenuItemResponse>(menuItem);
            response.CategoryText = category.Name;

            return response;
        }
    }
}
