namespace FilesUploaderApi.Repository;

public class MockRepository : IRepository
{
    private HashSet<BusinessUserEntity> Users { get; } = [];

    public CustomerSessionEntity AddCustomerSession(string userId, string sessionId)
    {
        var user = Users.FirstOrDefault(u => u.Name == userId);
        
        if(user == null)
            throw new KeyNotFoundException($"User with id {userId} not found");
        
        var session = Users.First(x => x.Name == userId).
            CustomerSessions.FirstOrDefault(x => x.Id == sessionId);
        
        if(session != null)
            throw new KeyNotFoundException($"CustomerSession with id {sessionId} already exists");
        
        var newSession = new CustomerSessionEntity { Id = sessionId };
        
        Users.First(x => x.Name == userId).CustomerSessions.Add(
            newSession);
        
        return newSession;
    }

    public bool BusinessUserExists(string userName)
    {
        return Users.Any(u => u.Name == userName);
    }

    public CustomerSessionEntity GetCustomerSession(string userId, string sessionId)
    {
        var user = Users.FirstOrDefault(u => u.Name == userId);
        
        if(user == null)
            throw new KeyNotFoundException($"User with id {userId} not found");

        var session = user.CustomerSessions.FirstOrDefault(x => x.Id == sessionId);
        
        if(session == null)
            throw new KeyNotFoundException($"CustomerSession with id {sessionId} not found");

        return session;
    }

    public async Task<string> AddBusinessUserAsync(string userId)
    {
        await Task.Run(() =>
        {
            if(BusinessUserExists(userId))
                throw new InvalidOperationException($"User with id {userId} already exists");
            
            Users.Add(new BusinessUserEntity
            {
                Name = userId
            });
        });

        return userId;
    }
}