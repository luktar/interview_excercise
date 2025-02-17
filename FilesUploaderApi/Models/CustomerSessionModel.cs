namespace FilesUploaderApi.Models;

public class CustomerSessionModel
{
    public List<string> Files { get; set; }
    public string Id { get; set; }
    public bool UploadCompleted { get; set; }
}