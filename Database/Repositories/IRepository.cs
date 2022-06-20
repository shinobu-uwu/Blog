using Blog.Models;

namespace Blog.Database.Repositories;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T model);
    void Remove(int id);
    void Save();
}