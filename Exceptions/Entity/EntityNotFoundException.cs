namespace Blog.Exceptions.Entity;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base("Could not find entity")
    {
    }

    public EntityNotFoundException(string message) : base(message)
    {
    }
}