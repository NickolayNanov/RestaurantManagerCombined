using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Services.Interfaces
{
    public interface IImageUploadService
    {
        Task<string> UploadAsync(UploadFile file, string folder, CancellationToken cancellationToken);
    }
}
