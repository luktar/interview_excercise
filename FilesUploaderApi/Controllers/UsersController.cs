using FilesUploaderApi.Models;
using FilesUploaderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilesUploaderApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUsersService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] NewBusinessUserModel userModel)
    {
        return Ok(new
        {
            Message = "User created successfully!",
            UserId = await service.AddUserAsync(userModel)
        });
    }
}