using FilesUploaderApi.Models;

namespace FilesUploaderApi.Services;

public interface IUploadService
{
    Task<CustomerSessionModel> UploadFilesAsync(string userId, string customerSessionId, IFormFile[]? files);
}