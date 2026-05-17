using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.UserDetails.GetByUserId
{
    internal class GetProfileDetailsByUserIdHandler(
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetProfileDetailsByUserIdQuery, GetProfileDetailsByUserIdResponse>
    {
        public async Task<GetProfileDetailsByUserIdResponse> Handle(GetProfileDetailsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var profileDetails = await dbContext.ProfileDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(ProfileDetails), $"No profile details for user id: {request.UserId}");

            return new GetProfileDetailsByUserIdResponse(
                profileDetails.Id,
                profileDetails.UserId,
                profileDetails.ProfilePictureUrl,
                profileDetails.FirstName,
                profileDetails.Surname,
                profileDetails.LastName,
                profileDetails.CompanyName,
                profileDetails.PhoneNumber);
        }
    }
}
