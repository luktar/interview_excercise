namespace FilesUploaderApi.Repository;

public interface IRepository
{
    public CustomerSessionEntity AddCustomerSession(string userId, string sessionId);
    public CustomerSessionEntity GetCustomerSession(string userId, string sessionId);
    public bool BusinessUserExists(string userName);
    public Task<string> AddBusinessUserAsync(string userId);
}