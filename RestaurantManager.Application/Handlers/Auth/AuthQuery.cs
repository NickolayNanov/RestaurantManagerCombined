using MediatR;

namespace RestaurantManager.Application.Handlers.Auth
{
    public record AuthQuery(string Email, string Password, bool RememberMe) : IRequest<AuthResponse>;
}
