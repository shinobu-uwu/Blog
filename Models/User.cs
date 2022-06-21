using Microsoft.AspNetCore.Identity;

namespace Blog.Models;

public class User : IdentityUser<int>
{
    public bool Enabled { get; set; }
    public DateTime CreationDate { get; init; }
}