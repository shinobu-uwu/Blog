using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class UserViewModel
{
    [Required(ErrorMessage = "You must inform a user name")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "You must specify an email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}