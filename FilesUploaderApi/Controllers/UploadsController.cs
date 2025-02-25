using FilesUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadsController(IUploadService service) : ControllerBase
{
    [HttpPost("{customerSessionId}")]
    public async Task<IActionResult> UploadFilesAsync(
        [FromRoute] string customerSessionId, 
        [FromForm] IFormFile[]? files)
    {
        return Ok(new
        {
            Message = "Files uploaded successfully!",
            Model = await service.UploadFilesAsync(customerSessionId, files)
        });
    }
}