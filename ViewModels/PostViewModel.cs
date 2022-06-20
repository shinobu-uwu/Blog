using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class PostViewModel
{
    [Required(ErrorMessage = "You must give your post a title")]
    [MaxLength(20, ErrorMessage = "Title must be up to 20 characters long")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters long")]
    public string Title { get; set; }
    public string Body { get; set; }
}