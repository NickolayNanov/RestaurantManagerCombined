using MediatR;
using Microsoft.AspNetCore.Identity;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Users.GetById
{
    internal class GetUserByIdHandler(
        UserManager<ApplicationUser> userManager) : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse>
    {
        public async Task<GetUserByIdResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            
            if (user == null)
            {
                throw new ResourceNotFoundException(nameof(ApplicationUser), $"User with id: {request.Id} does not exist.");
            }

            return new GetUserByIdResponse
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email
            };
        }
    }
}
