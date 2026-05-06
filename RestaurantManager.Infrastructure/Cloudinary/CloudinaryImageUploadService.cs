using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Infrastructure.Cloudinary
{
    public sealed class CloudinaryImageUploadService : IImageUploadService
    {
        private readonly CloudinaryDotNet.Cloudinary cloudinary;

        public CloudinaryImageUploadService(IOptions<CloudinaryOptions> options)
        {
            var cloudinaryOptions = options.Value;

            var account = new Account(
                cloudinaryOptions.CloudName,
                cloudinaryOptions.ApiKey,
                cloudinaryOptions.ApiSecret);

            cloudinary = new CloudinaryDotNet.Cloudinary(account)
            {
                Api =
                {
                    Secure = true
                }
            };
        }

        public async Task<string> UploadAsync(UploadFile file, string folder, CancellationToken cancellationToken)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.Stream),
                Folder = folder,
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false
            };

            var result = await cloudinary.UploadAsync(uploadParams, cancellationToken);

            if (result.Error is not null)
            {
                throw new InvalidOperationException(result.Error.Message);
            }

            return result.SecureUrl?.ToString()
                ?? throw new InvalidOperationException("Cloudinary did not return a secure image URL.");
        }
    }
}
