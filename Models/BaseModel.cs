namespace Blog.Models;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTime CreationDate { get; init; }
    public bool Enabled { get; set; }
}