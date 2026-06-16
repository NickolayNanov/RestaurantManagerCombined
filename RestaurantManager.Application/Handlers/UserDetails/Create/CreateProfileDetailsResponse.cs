namespace RestaurantManager.Application.Handlers.UserDetails.Create
{
    public record CreateProfileDetailsResponse(
        Guid Id,
        string UserId,
        string ProfilePictureUrl,
        string FirstName,
        string Surname,
        string LastName,
        string CompanyName,
        string PhoneNumber);
}
