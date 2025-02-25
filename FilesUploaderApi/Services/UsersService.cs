using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FilesUploaderApi.Services;

public class UsersService(IRepository repository) : IUsersService
{
    public Task<string> AddUserAsync(NewBusinessUserModel userModel)
    {
        if (repository.BusinessUserExists(userModel.UserName))
            throw new ArgumentException($"User with name {userModel.UserName} already exists");
        
        return repository.AddBusinessUserAsync(userModel.UserName);

    }
}