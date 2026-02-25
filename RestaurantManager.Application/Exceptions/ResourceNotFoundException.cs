namespace RestaurantManager.Application.Exceptions
{
    public class ResourceNotFoundException(string resourceName, string message) : Exception(message)
    {
        public string ResourceName { get; set; } = resourceName;
    }
}
