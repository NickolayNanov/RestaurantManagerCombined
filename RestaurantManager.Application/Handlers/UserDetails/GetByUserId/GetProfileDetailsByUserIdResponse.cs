namespace RestaurantManager.Application.Handlers.UserDetails.GetByUserId
{
    public record GetProfileDetailsByUserIdResponse(
        Guid Id,
        string UserId,
        string ProfilePictureUrl,
        string FirstName,
        string Surname,
        string LastName,
        string CompanyName,
        string PhoneNumber);
}
