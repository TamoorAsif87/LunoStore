using Microsoft.AspNetCore.Http;

namespace Shared.Services.Contracts;

public interface IFileUpload
{
    Task<string> UploadImageAsync(IFormFile file);
}
