using FilesUploaderApi.Models;

namespace FilesUploaderApi.Services;

public interface ICustomerSessionService
{
    CustomerSessionModel GetSessionState(string userId, string customerSessionId);
    CustomerSessionModel CreateSession(string userId, string customerSessionId);
}