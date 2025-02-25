using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;

namespace FilesUploaderApi.Services;

public class CustomerSessionService(IRepository repository) : ICustomerSessionService
{
    public CustomerSessionModel GetSessionState(string userId, string customerSessionId)
    {
        var session = repository.GetCustomerSession(userId, customerSessionId);

        return new CustomerSessionModel
        {
            Id = session.Id,
            UploadCompleted = session.UploadCompleted,
            Files = session.Files
        };
    }

    public CustomerSessionModel CreateSession(string userId, string customerSessionId)
    {
        var entity =
            repository.AddCustomerSession(userId, customerSessionId);
        
        return new CustomerSessionModel
        {
            Id = entity.Id,
            Files = entity.Files,
            UploadCompleted = entity.UploadCompleted
        };
    }
}