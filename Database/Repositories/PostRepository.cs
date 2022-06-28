using Blog.Exceptions.Entity;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

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
        var user = _dbContext.Posts
            .Where(post => post.Id == id)
            .Include(p => p.Author)
            .First();

        if (user is null)
        {
            throw new EntityNotFoundException($"Could not find post of id ${id}");
        }

        return user;
    }

    public IEnumerable<Post> GetAll()
    {
        return _dbContext.Posts.Include(p => p.Author);
    }

    public IEnumerable<Post> GetAllEnabled()
    {
        return _dbContext.Posts
            .Where(p => p.Enabled == true)
            .Include(p => p.Author);
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
        return _dbContext.Posts
            .Where(p => p.Enabled == true)
            .OrderBy(p => p.CreationDate);
    }

    public void Disable(Post model)
    {
        model.Enabled = false;
    }
}