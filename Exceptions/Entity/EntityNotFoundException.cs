namespace Blog.Exceptions.Entity;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base("Could not find entity")
    {
    }

    public EntityNotFoundException(string message) : base(message)
    {
    }

    public EntityNotFoundException(string entityName, string entityId) : base(
        $"{entityName} of identifier {entityId} could not be found")
    {
    }
}