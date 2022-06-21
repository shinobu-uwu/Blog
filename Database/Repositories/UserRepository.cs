using Blog.Exceptions.Entity;
using Blog.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.Database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BlogDbContext _dbContext;

    public UserRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetById(int id)
    {
        var user = _dbContext.Users.Find(id);

        if (user is null)
        {
            throw new EntityNotFoundException("User", id.ToString());
        }

        return user;
    }

    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users;
    }

    public void Add(User model)
    {
        _dbContext.Users.Add(model);
    }

    public void Remove(int id)
    {
        _dbContext.Users.Remove(new User{Id = id});
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}