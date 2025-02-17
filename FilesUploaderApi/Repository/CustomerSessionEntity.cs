namespace FilesUploaderApi.Repository;

public class CustomerSessionEntity
{
    private static List<string> _requiredFileNames = ["File1.pdf", "File2.pdf", "File3.pdf"];
    public string Id { get; set; }
    public List<string> Files { get; set; } = [];
    public bool MessageSent { get; set; } = false;

    public bool UploadCompleted => _requiredFileNames.All(Files.Contains);
}