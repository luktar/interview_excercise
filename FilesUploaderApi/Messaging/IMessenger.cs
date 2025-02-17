namespace FilesUploaderApi.Messaging;

public interface IMessenger
{
    public bool Send(string message);
}