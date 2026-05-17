namespace RestaurantManager.Application.Handlers.UserDetails.Update
{
    public record UpdateProfileDetailsResponse(
        Guid Id,
        string UserId,
        string ProfilePictureUrl,
        string FirstName,
        string Surname,
        string LastName,
        string CompanyName,
        string PhoneNumber);
}
