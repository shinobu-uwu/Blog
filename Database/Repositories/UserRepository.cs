using Microsoft.AspNetCore.Identity;

namespace Blog.Database.Repositories;

public class UserRepository : IUserRepository
{
    public IdentityUser GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IdentityUser> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IdentityUser> GetAllEnabled()
    {
        throw new NotImplementedException();
    }

    public void Add(IdentityUser model)
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}