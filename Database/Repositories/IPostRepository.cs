using Blog.Models;

namespace Blog.Database.Repositories;

public interface IPostRepository : IRepository<Post>
{
    IEnumerable<Post> GetAllEnabledOrderedByDate();
    IEnumerable<Post> GetAllEnabledUserPostsOrderedByDate(int userId);
    void Disable(Post post);
}