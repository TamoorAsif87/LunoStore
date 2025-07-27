using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Shared.Services.Contracts;

namespace Shared.Services.CloudinaryService;

public class CloudinaryService(Cloudinary _cloudinary):IFileUpload
{
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file.Length == 0 || file == null)
        {
            throw new ArgumentException("File is empty", nameof(file));
        }

        await using var stream = file.OpenReadStream();


        var uploadParams = new  ImageUploadParams
        {
            File = new FileDescription(file.FileName,stream),
            Transformation = new Transformation().Crop("fill").Gravity("face")
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            return uploadResult.SecureUrl.ToString();

        throw new ApplicationException("Image upload failed");
    }
}
