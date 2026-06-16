namespace RestaurantManager.Application.Handlers.UserDetails
{
    public abstract record ProfileDetailsBase
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
