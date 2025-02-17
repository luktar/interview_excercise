using FilesUploaderApi.Messaging;
using JetBrains.Annotations;
using Xunit;

namespace FilesUploaderApi.Tests.Messaging;

[TestSubject(typeof(ConsoleMessenger))]
public class ConsoleMessengerTest
{

    [Fact]
    public void SendingMessageTest()
    {
        IMessenger consoleMessenger = new ConsoleMessenger();
        Assert.True(consoleMessenger.Send("Hello World!"));
    }
}