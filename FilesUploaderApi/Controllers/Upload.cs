using FilesUploaderApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController(IRepository repository) : ControllerBase
{
    private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    [HttpGet]
    public IActionResult Get() => Ok("UploadController is working!");
    
    [HttpPost("{customerSessionId}")]
    public async Task<IActionResult> UploadFiles(
        [FromRoute] string customerSessionId, 
        [FromForm] IFormFile[]? files)
    {
        if(!repository.CustomerSessionExists(customerSessionId))
            return NotFound($"Customer session with ID {customerSessionId} not found.");
        
        if (files == null || files.Length == 0)
            return BadRequest("No files uploaded.");

        var customerSession = repository.GetCustomerSession(customerSessionId);

        await SaveFiles(files, customerSessionId);
        UpdateCustomerSession(files, customerSession);

        if (customerSession is { UploadCompleted: true, MessageSent: false })
        {
            // Send message
            customerSession.MessageSent = true;
        }

        return Ok(new
        {
            Message = "Files uploaded successfully!",
            CustomerSession = customerSession
        });
    }

    private static void UpdateCustomerSession(IFormFile[] files, CustomerSessionEntity customerSession)
    {
        customerSession.Files.AddRange(files.Select(x => x.FileName));
    }
    
    private async Task SaveFiles(IFormFile[] files, string customerSessionId)
    {
        string customerDirectory = Path.Combine(_storagePath, customerSessionId);
        if (!Directory.Exists(customerDirectory))
        {
            Directory.CreateDirectory(customerDirectory);
        }
        
        foreach (var file in files)
        {
            var filePath = Path.Combine(customerDirectory, customerSessionId, file.FileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}