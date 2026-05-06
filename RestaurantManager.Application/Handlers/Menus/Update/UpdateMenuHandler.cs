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

namespace RestaurantManager.Application.Handlers.Menus.Update
{
    internal class UpdateMenuHandler(
        IRestaurantManagerDbContext dbContext,
        IMapper mapper,
        ICurrentUserService currentUserService,
        ILogger<CreateMenuHandler> logger,
        IImageUploadService imageUploadService) : IRequestHandler<UpdateMenuCommand, UpdateMenuResponse>
    {
        public async Task<UpdateMenuResponse> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await dbContext.Menus.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(Menu), $"Menu with id {request.Id} was not found when trying to update.");

            menu.Name = request.Name;
            menu.Description = request.Description;
            menu.IsActive = request.IsActive;
            menu.Type = request.Type;
            menu.UpdatedAt = DateTime.UtcNow;
            menu.UpdatedBy = currentUserService.UserId;

            if (request.Image is not null)
            {
                menu.ImgUrl = await imageUploadService.UploadAsync(
                    request.Image,
                    ImageUploadFolders.Menus,
                    cancellationToken);
            }

            dbContext.Menus.Update(menu);

            logger.LogInformation($"Updated menu with id {menu.Id}.");
            var response = mapper.Map<UpdateMenuResponse>(menu);

            return response;
        }
    }
}
