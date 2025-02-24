using FilesUploaderApi.Models;

namespace FilesUploaderApi.Services;

public interface ICustomerSessionService
{
    CustomerSessionModel GetSessionState(string customerSessionId);
    CustomerSessionModel CreateSession(NewSessionModel newSession);
}