namespace Blog.Models;

public class Post : BaseModel
{
    public string Title { get; set; }
    public string Body { get; set; }

    public Post(string title, string body)
    {
        Title = title;
        Body = body;
        CreationDate = DateTime.Now;
        Enabled = true;
    }
}