using FilesUploaderApi.Models;

namespace FilesUploaderApi.Services;

public interface IUsersService
{
    Task<string> AddUserAsync(NewBusinessUserModel userModel);
}