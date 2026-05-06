using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.MenuItems.Create
{
    internal class CreateMenuItemHandler(
        IRestaurantManagerDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateMenuItemHandler> logger,
        IImageUploadService imageUploadService) : IRequestHandler<CreateMenuItemCommand, CreateMenuItemResponse>
    {
        public async Task<CreateMenuItemResponse> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menu = await dbContext.Menus.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.MenuId, cancellationToken);

            if (menu is null)
            {
                logger.LogWarning("Menu with id {MenuId} not found", request.MenuId);
                throw new ResourceNotFoundException(nameof(Menu), $"Menu with id {request.MenuId} not found");
            }

            var category = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (category is null)
            {
                logger.LogWarning("Category with id {MenuId} not found", request.MenuId);
                throw new ResourceNotFoundException(nameof(Category), $"Category with id {request.CategoryId} not found");
            }

            var menuItem = mapper.Map<MenuItem>(request);
            menuItem.ImgUrl = await imageUploadService.UploadAsync(
                request.Image,
                ImageUploadFolders.MenuItems,
                cancellationToken);

            menuItem.CreatedAt = DateTime.UtcNow;
            menuItem.CreatedBy = currentUserService.UserId;

            dbContext.MenuItems.Add(menuItem);

            var response = mapper.Map<CreateMenuItemResponse>(menuItem);
            response.CategoryText = category.Name;

            return response;
        }
    }
}
