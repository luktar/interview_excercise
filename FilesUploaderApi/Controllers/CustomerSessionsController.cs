using FilesUploaderApi.Models;
using FilesUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/users/{userId}/customerSessions")]
public class CustomerSessionsController(ICustomerSessionService service) : ControllerBase
{
    [HttpGet("{customerSessionId}")]
    public IActionResult GetSessionState([FromRoute] string userId, [FromRoute] string customerSessionId)
    {
        return Ok(service.GetSessionState(userId, customerSessionId));
    }

    [HttpPost]
    public IActionResult CreateSession([FromRoute] string userId, [FromBody] NewSessionModel newSession)
    {
        return Ok(new
        {
            Message = $"Session '{newSession.CustomerSessionId}' successfully created!",
            Session = service.CreateSession(userId, newSession.CustomerSessionId)
        });
    }
}