using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;

namespace FilesUploaderApi.Services;

public class CustomerSessionService(IRepository repository) : ICustomerSessionService
{
    public CustomerSessionModel GetSessionState(string customerSessionId)
    {
        if (!repository.CustomerSessionExists(customerSessionId))
            throw new KeyNotFoundException($"Customer session with ID '{customerSessionId}' not found.");

        var session = repository.GetCustomerSession(customerSessionId);

        return new CustomerSessionModel
        {
            Id = session.Id,
            UploadCompleted = session.UploadCompleted,
            Files = session.Files
        };
    }

    public CustomerSessionModel CreateSession(NewSessionModel newSession)
    {
        if (!repository.BusinessUserExists(newSession.BusinessUserName))
            throw new KeyNotFoundException($"User with ID '{newSession.BusinessUserName}' not found.");

        if (repository.CustomerSessionExists(newSession.CustomerSessionId))
            throw new InvalidOperationException($"Customer session with ID '{newSession.CustomerSessionId}' already exists.");

        var entity =
            repository.AddCustomerSession(newSession.BusinessUserName, newSession.CustomerSessionId);
        
        return new CustomerSessionModel
        {
            Id = entity.Id,
            Files = entity.Files,
            UploadCompleted = entity.UploadCompleted
        };
    }
}