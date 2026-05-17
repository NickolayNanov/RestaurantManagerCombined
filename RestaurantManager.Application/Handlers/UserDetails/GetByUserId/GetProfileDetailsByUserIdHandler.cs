using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.UserDetails.GetByUserId
{
    internal class GetProfileDetailsByUserIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetProfileDetailsByUserIdQuery, GetProfileDetailsByUserIdResponse>
    {
        public async Task<GetProfileDetailsByUserIdResponse> Handle(GetProfileDetailsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.ProfileDetails
                .AsNoTracking()
                .ProjectTo<GetProfileDetailsByUserIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(ProfileDetails), $"No profile details for user id: {request.UserId}");
        }
    }
}
