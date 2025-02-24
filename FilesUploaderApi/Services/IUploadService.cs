using FilesUploaderApi.Models;

namespace FilesUploaderApi.Services;

public interface IUploadService
{
    Task<CustomerSessionModel> UploadFilesAsync(string customerSessionId, IFormFile[]? files);
}