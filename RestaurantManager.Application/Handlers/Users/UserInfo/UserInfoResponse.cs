namespace RestaurantManager.Application.Handlers.Users.UserInfo
{
    public record UserInfoResponse(string Id, string Username, string Email, IEnumerable<string> Roles, string ProfilePictureUrl);
}
