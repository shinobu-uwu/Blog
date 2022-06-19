using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Database;

public class BlogDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Post> Posts { get; set; }

    public BlogDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
