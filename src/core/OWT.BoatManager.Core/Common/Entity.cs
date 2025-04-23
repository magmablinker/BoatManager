namespace OWT.BoatManager.Core.Common;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}