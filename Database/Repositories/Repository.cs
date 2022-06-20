using Blog.Exceptions.Entity;

namespace Blog.Database.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly BlogDbContext _dbContext;

    public Repository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public T GetById(int id)
    {
        var model = _dbContext.Find<T>(id);

        if (model is null)
        {
            throw new EntityNotFoundException();
        }

        return model;
    }

    public IEnumerable<T> GetAll()
    {
        return _dbContext.Set<T>();
    }

    public void Add(T model)
    {
        _dbContext.Add<T>(model);
    }

    public void Remove(int id)
    {
        var model = _dbContext.Find<T>(id);

        if (model is null)
        {
            throw new EntityNotFoundException();
        }

        _dbContext.Remove<T>(model);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}