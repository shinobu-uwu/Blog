using Blog.Exceptions.Entity;
using Blog.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Blog.Database.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _dbContext;

    public PostRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Post GetById(int id)
    {
        var user = _dbContext.Posts.Find(id);

        if (user is null)
        {
            throw new EntityNotFoundException($"Could not find post of id ${id}");
        }

        return user;
    }

    public IEnumerable<Post> GetAll()
    {
        return _dbContext.Posts;
    }

    public IEnumerable<Post> GetAllEnabled()
    {
        return _dbContext.Posts.Where(p => p.Enabled == true);
    }

    public void Add(Post model)
    {
        _dbContext.Posts.Add(model);
    }

    public void Remove(int id)
    {
        _dbContext.Remove(new { Id = id });
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public IEnumerable<Post> GetAllEnabledOrderedByDate()
    {
        return _dbContext.Posts.Where(p => p.Enabled == true).OrderBy(p => p.CreationDate);
    }
}