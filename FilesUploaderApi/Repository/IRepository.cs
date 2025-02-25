namespace FilesUploaderApi.Repository;

public interface IRepository
{
    public bool CustomerSessionExists(string sessionId);
    public CustomerSessionEntity AddCustomerSession(string userName, string sessionId);
    public CustomerSessionEntity GetCustomerSession(string sessionId);
    public bool BusinessUserExists(string userName);
    public Task<string> AddBusinessUserAsync(string userName);
}