namespace FilesUploaderApi.Messaging;

public interface IMessaging
{
    public bool Send(string message);
}