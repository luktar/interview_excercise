using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace FilesUploaderApi.Repository;

public class MockRepository : IRepository
{
    public HashSet<CustomerSessionEntity> Sessions { get; }

    public HashSet<BusinessUserEntity> Users { get; } =
    [
        new BusinessUserEntity
        {
            Name = "user_1", 
            CustomerSessions = [
                new CustomerSessionEntity
                {
                    Id = "customer_session_1"
                },
                new CustomerSessionEntity
                {
                    Id = "customer_session_2"
                },
                new CustomerSessionEntity
                {
                    Id = "customer_session_3"
                }
            ]
        },
        new BusinessUserEntity
        {
            Name = "user_2", 
            CustomerSessions = [
                new CustomerSessionEntity
                {
                    Id = "customer_session_4"
                },
                new CustomerSessionEntity
                {
                    Id = "customer_session_5"
                }
            ]
        }
    ];

    public MockRepository()
    {
        Sessions = Users.SelectMany(u => u.CustomerSessions).ToHashSet();
    }

    public bool CustomerSessionExists(string sessionId)
    {
        return Sessions.Any(x => x.Id == sessionId);
    }
    
    public CustomerSessionEntity AddCustomerSession(string userName, string sessionId)
    {
        var session = new CustomerSessionEntity { Id = sessionId };
        Users.First(x => x.Name == userName).CustomerSessions.Add(
            session);
        return session;
    }

    public bool BusinessUserExists(string userName)
    {
        return Users.Any(u => u.Name == userName);
    }

    public CustomerSessionEntity GetCustomerSession(string sessionId)
    {
        return Sessions.First(x => x.Id == sessionId);
    }
}