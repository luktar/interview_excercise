using FilesUploaderApi.Messaging;
using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;
using FilesUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController(IUploadService service) : ControllerBase
{

    [HttpPost("{customerSessionId}")]
    public async Task<IActionResult> UploadFiles(
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