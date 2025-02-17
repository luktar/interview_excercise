namespace FilesUploaderApi.Messaging;

public class Email : IMessaging
{
    public bool Send(string message)
    {
        Console.WriteLine("Sending email:");
        Console.WriteLine(message);
        return true;
    }
}