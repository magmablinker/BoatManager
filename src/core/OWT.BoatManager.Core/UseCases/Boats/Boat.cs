using OWT.BoatManager.Core.Common;

namespace OWT.BoatManager.Core.UseCases.Boats;
public sealed class Boat : Entity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}
