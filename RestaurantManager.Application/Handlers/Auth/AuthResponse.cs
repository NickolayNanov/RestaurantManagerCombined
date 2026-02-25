namespace RestaurantManager.Application.Handlers.Auth
{
    public record AuthResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken = null, DateTime? RefreshTokenExpiration = null);
}
