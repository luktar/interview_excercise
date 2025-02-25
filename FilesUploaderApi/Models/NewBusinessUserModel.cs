using System.ComponentModel.DataAnnotations;

namespace FilesUploaderApi.Models;

public class NewBusinessUserModel
{
    [Required]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "BusinessUserName must be between 5 and 100 characters.")]
    public string UserName { get; set; }
}