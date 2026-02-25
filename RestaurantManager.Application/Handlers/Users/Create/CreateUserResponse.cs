namespace RestaurantManager.Application.Handlers.Users.Create
{
    public record CreateUserResponse
    {
        public string Id { get; set; }

        public string Username { get; set; }
    }
}
