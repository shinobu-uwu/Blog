using Microsoft.AspNetCore.Identity;

namespace Blog.Database.Repositories;

public interface IUserRepository : IRepository<IdentityUser>
{
    
}