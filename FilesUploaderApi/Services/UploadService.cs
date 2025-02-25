using FilesUploaderApi.Messaging;
using FilesUploaderApi.Models;
using FilesUploaderApi.Repository;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace FilesUploaderApi.Services;

public class UploadService(IRepository repository, IMessenger messenger) : IUploadService
{
    private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

    
    public async Task<CustomerSessionModel> UploadFilesAsync(string userId, string customerSessionId, IFormFile[]? files)
    {
        if (files == null || files.Length == 0)
            throw new ArgumentException("No files uploaded.");

        var customerSession = repository.GetCustomerSession(userId, customerSessionId);

        await SaveFiles(files, customerSessionId);
        UpdateCustomerSession(files, customerSession);

        if (customerSession is { UploadCompleted: true, MessageSent: false })
        {
            messenger.Send($"Required files successfully uploaded for customer session ID '{customerSessionId}'");
            customerSession.MessageSent = true;
        }

        return new CustomerSessionModel
            {
                Id = customerSessionId,
                Files = customerSession.Files,
                UploadCompleted = customerSession.UploadCompleted
            };
    }
    
    private static void UpdateCustomerSession(IFormFile[] files, CustomerSessionEntity customerSession)
    {
        var existingFiles = new HashSet<string>(customerSession.Files);

        foreach (var file in files)
        {
            if (existingFiles.Contains(file.FileName)) continue;
            customerSession.Files.Add(file.FileName);
            existingFiles.Add(file.FileName);
        }
    }
    
    private async Task SaveFiles(IFormFile[] files, string customerSessionId)
    {
        string customerDirectory = Path.Combine(_storagePath, $"{customerSessionId}/");
        if (!Directory.Exists(customerDirectory))
        {
            Directory.CreateDirectory(customerDirectory);
        }
        
        foreach (var file in files)
        {
            var filePath = Path.Combine(customerDirectory, file.FileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
    }
}