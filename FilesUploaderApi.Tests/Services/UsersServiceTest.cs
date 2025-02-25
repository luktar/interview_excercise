using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;
using FilesUploaderApi.Services;
using System;
using System.Threading.Tasks;
using Xunit;
using MockRepository = FilesUploaderApi.Repository.MockRepository;

namespace FilesUploaderApi.Tests.Services;

public class UsersServiceTests
{
    private readonly UsersService _usersService;

    public UsersServiceTests()
    {
        IRepository repository = new MockRepository();
        repository.AddBusinessUserAsync("existing_user");
        
        _usersService = new UsersService(repository);
    }

    [Fact]
    public async Task AddUserAsync_ShouldThrowArgumentException_WhenUserExists()
    {
        var userModel = new NewBusinessUserModel { UserName = "existing_user" };

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _usersService.AddUserAsync(userModel));
        Assert.Equal($"User with name {userModel.UserName} already exists", exception.Message);
    }

    [Fact]
    public async Task AddUserAsync_ShouldReturnBusinessUserId_WhenSuccessful()
    {
        var userModel = new NewBusinessUserModel { UserName = "new_user_id" };
        var expectedUserId = "new_user_id";

        var result = await _usersService.AddUserAsync(userModel);
        
        Assert.Equal(expectedUserId, result);
    }
}
