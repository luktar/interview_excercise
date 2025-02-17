using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerSession(IRepository repository) : ControllerBase
{
    [HttpGet("{customerSessionId}")]
    public IActionResult GetSessionState([FromRoute] string customerSessionId)
    {
        if (!repository.CustomerSessionExists(customerSessionId))
            return NotFound($"Customer session with ID '{customerSessionId}' not found.");

        var session = repository.GetCustomerSession(customerSessionId);

        return Ok(new CustomerSessionModel
        {
            Id = session.Id,
            UploadCompleted = session.UploadCompleted,
            Files = session.Files
        });
    }

    [HttpPost]
    public IActionResult CreateSession([FromBody] NewSessionModel newSession)
    {
        if (!repository.BusinessUserExists(newSession.BusinessUserName))
            return NotFound($"User with ID '{newSession.BusinessUserName}' not found.");

        if (repository.CustomerSessionExists(newSession.CustomerSessionId))
            return BadRequest($"Customer session with ID '{newSession.CustomerSessionId}' already exists.");

        var entity =
            repository.AddCustomerSession(newSession.BusinessUserName, newSession.CustomerSessionId);
        
        var model = new CustomerSessionModel
        {
            Id = entity.Id,
            Files = entity.Files,
            UploadCompleted = entity.UploadCompleted
        };
        
        return Ok(new
        {
            Message = $"Session '{newSession.CustomerSessionId}' successfully created!",
            Session = model
        });
    }
}