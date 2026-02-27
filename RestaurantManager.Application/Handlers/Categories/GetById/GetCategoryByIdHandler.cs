using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Categories.GetById
{
    internal class GetCategoryByIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
    {
        public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await dbContext.Categories
                .AsSplitQuery()
                .AsNoTracking()
                .ProjectTo<GetCategoryByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);

            if (category == null) 
            {
                throw new ResourceNotFoundException(nameof(Category), $"Category with id: {request.id} was not found.");
            }

            return category;
        }
    }
}
