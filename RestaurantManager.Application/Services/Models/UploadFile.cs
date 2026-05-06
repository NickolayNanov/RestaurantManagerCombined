namespace RestaurantManager.Application.Services.Models
{
    public sealed record UploadFile(
        Stream Stream,
        string FileName,
        string ContentType,
        long Length);
}
