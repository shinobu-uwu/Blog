using Blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Database;

public class BlogDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Post> Posts { get; set; }

    public BlogDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(b => { b.ToTable("Users"); });
        builder.Entity<IdentityUserClaim<int>>(b => { b.ToTable("UserClaims"); });
        builder.Entity<IdentityUserLogin<int>>(b => { b.ToTable("UserLogins"); });
        builder.Entity<IdentityUserToken<int>>(b => { b.ToTable("UserTokens"); });
        builder.Entity<IdentityRole>(b => { b.ToTable("Roles"); });
        builder.Entity<IdentityRoleClaim<int>>(b => { b.ToTable("RoleClaims"); });
        builder.Entity<IdentityUserRole<int>>(b => { b.ToTable("UserRoles"); });

        base.OnModelCreating(builder);
    }
}