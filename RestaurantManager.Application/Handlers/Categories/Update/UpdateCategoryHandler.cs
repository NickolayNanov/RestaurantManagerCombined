using AutoMapper;
using MediatR;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Categories.Update
{
    internal class UpdateCategoryHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext
        ) : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
    {
        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await restaurantManagerDbContext.Categories.FindAsync(request.Id);

            if (category is null)
            {
                throw new KeyNotFoundException($"Category with Id {request.Id} not found.");
            }

            mapper.Map(request, category);

            restaurantManagerDbContext.Categories.Update(category);

            var response = mapper.Map<UpdateCategoryResponse>(category);

            return response;
        }
    }
}
