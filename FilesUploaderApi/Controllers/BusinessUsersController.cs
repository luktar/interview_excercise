using FilesUploaderApi.Models;
using FilesUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessUsersController(IUsersService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] NewBusinessUserModel userModel)
    {
        return Ok(new
        {
            Message = "User created successfully!",
            BusinessUserId = await service.AddUserAsync(userModel)
        });
    }
}