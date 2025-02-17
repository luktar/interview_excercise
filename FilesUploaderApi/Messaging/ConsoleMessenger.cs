namespace FilesUploaderApi.Messaging;

public class ConsoleMessenger : IMessenger
{
    public bool Send(string message)
    {
        Console.WriteLine("Sending console message:");
        Console.WriteLine(message);
        return true;
    }
}