namespace RestaurantManager.Application.Handlers.Users.Me
{
    public record UserInfoResponse(string Id, string Username, string Email, IEnumerable<string> Roles);
}
