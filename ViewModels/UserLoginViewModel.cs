using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class UserLoginViewModel
{
    [Required(ErrorMessage = "You must inform a user name")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}