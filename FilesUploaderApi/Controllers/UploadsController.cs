using FilesUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/users/{userId}/customerSessions/{customerSessionId}/uploads")]
public class UploadsController(IUploadService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFilesAsync(
        [FromRoute] string userId,
        [FromRoute] string customerSessionId, 
        [FromForm] IFormFile[]? files)
    {
        return Ok(new
        {
            Message = "Files uploaded successfully!",
            Model = await service.UploadFilesAsync(userId, customerSessionId, files)
        });
    }
}