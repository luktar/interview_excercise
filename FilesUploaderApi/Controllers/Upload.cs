using FilesUploaderApi.Messaging;
using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController(IRepository repository, IMessenger messenger) : ControllerBase
{
    private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
    
    [HttpPost("{customerSessionId}")]
    public async Task<IActionResult> UploadFiles(
        [FromRoute] string customerSessionId, 
        [FromForm] IFormFile[]? files)
    {
        if(!repository.CustomerSessionExists(customerSessionId))
            return NotFound($"Customer session with ID '{customerSessionId}' not found.");
        
        if (files == null || files.Length == 0)
            return BadRequest("No files uploaded.");

        var customerSession = repository.GetCustomerSession(customerSessionId);

        await SaveFiles(files, customerSessionId);
        UpdateCustomerSession(files, customerSession);

        if (customerSession is { UploadCompleted: true, MessageSent: false })
        {
            messenger.Send($"Required files successfully uploaded for customer session ID '{customerSessionId}'");
            customerSession.MessageSent = true;
        }

        return Ok(new
        {
            Message = "Files uploaded successfully!",
            CustomerSession = new CustomerSessionModel
            {
                Id = customerSessionId,
                Files = customerSession.Files,
                UploadCompleted = customerSession.UploadCompleted
            }
        });
    }

    private static void UpdateCustomerSession(IFormFile[] files, CustomerSessionEntity customerSession)
    {
        customerSession.Files.AddRange(files.Select(x => x.FileName));
    }
    
    private async Task SaveFiles(IFormFile[] files, string customerSessionId)
    {
        string customerDirectory = Path.Combine(_storagePath, $"{customerSessionId}/");
        if (!Directory.Exists(customerDirectory))
        {
            Directory.CreateDirectory(customerDirectory);
        }
        
        foreach (var file in files)
        {
            var filePath = Path.Combine(customerDirectory, file.FileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}