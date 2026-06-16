namespace RestaurantManager.Domain.Entities
{
    public class ProfileDetails : EntityBase
    {
        public string ProfilePictureUrl { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
