using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FilesUploaderApi.Services;

public class UsersService(IRepository repository) : IUsersService
{
    public Task<string> AddUserAsync(NewBusinessUserModel userModel)
    {
        if(repository.BusinessUserExists(userModel.UserName))
            throw new ArgumentException($"User with name {userModel.UserName} already exists");

        try
        {
            return repository.AddBusinessUserAsync(userModel.UserName);
        } catch(Exception e)
        {
            throw new InvalidOperationException($"Unable to create user with name {userModel.UserName}.");
        }
    }
}