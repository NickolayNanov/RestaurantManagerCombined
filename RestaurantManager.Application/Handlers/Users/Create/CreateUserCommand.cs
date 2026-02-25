using MediatR;

namespace RestaurantManager.Application.Handlers.Users.Create
{
    public record CreateUserCommand : IRequest<CreateUserResponse>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
