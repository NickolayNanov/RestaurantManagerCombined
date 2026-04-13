using AutoMapper;
using MediatR;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Categories.Create
{
    internal class CreateCategoryHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        public Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(request);

            category.CreatedAt = DateTime.UtcNow;
            category.CreatedBy = currentUserService.UserId;

            restaurantManagerDbContext.Categories.Add(category);

            var response = mapper.Map<CreateCategoryResponse>(category);

            return Task.FromResult(response);
        }
    }
}
