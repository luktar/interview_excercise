using System.ComponentModel.DataAnnotations;

namespace FilesUploaderApi.Models;

public class NewSessionModel
{
    [Required]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "CustomerSessionId must be between 5 and 100 characters.")]
    public string CustomerSessionId { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "BusinessUserName must be between 5 and 100 characters.")]
    public string BusinessUserName { get; set; }
}