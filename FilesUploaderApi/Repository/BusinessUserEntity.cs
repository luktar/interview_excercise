namespace FilesUploaderApi.Repository;

public class BusinessUserEntity
{
    public string Name { get; set; }
    public List<CustomerSessionEntity> CustomerSessions { get; set; }
}