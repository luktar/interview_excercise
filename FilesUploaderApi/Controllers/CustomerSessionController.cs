using FilesUploaderApi.Models;
using FilesUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerSessionController(ICustomerSessionService service) : ControllerBase
{
    [HttpGet("{customerSessionId}")]
    public IActionResult GetSessionState([FromRoute] string customerSessionId)
    {
        return Ok(service.GetSessionState(customerSessionId));
    }

    [HttpPost]
    public IActionResult CreateSession([FromBody] NewSessionModel newSession)
    {
        return Ok(new
        {
            Message = $"Session '{newSession.CustomerSessionId}' successfully created!",
            Session = service.CreateSession(newSession)
        });
    }
}