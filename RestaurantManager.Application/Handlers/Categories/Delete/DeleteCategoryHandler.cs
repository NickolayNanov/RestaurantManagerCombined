using MediatR;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Categories.Delete
{
    internal class DeleteCategoryHandler(
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
    {
        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await restaurantManagerDbContext.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

            if (category is null)
            {
                throw new ResourceNotFoundException(nameof(Category), $"Category with id: {request.Id} was not found.");
            }

            restaurantManagerDbContext.Categories.Remove(category);

            return new DeleteCategoryResponse(true);
        }
    }
}
