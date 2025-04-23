namespace OWT.BoatManager.Core.Common;
public interface IEntity
{
    Guid Id { get; set; }
    DateTimeOffset CreatedAt { get; set; }
}
