using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Services
{
    public static class ImageValidation
    {
        public const long MaxFileSize = 5 * 1024 * 1024;

        public static readonly string[] AllowedContentTypes =
        [
            "image/jpeg",
            "image/png",
            "image/webp"
        ];

        public static bool HasAllowedContentType(UploadFile file)
        {
            return file is not null &&
                AllowedContentTypes.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase);
        }
    }
}
