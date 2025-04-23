using OWT.BoatManager.Application.UseCases.Boats.Models;
using OWT.BoatManager.Core.UseCases.Boats;
using Riok.Mapperly.Abstractions;

namespace OWT.BoatManager.Application.UseCases.Boats.Commands.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
internal static partial class BoatMappingExtensions
{
    [MapperIgnoreTarget(nameof(Boat.Id))]
    [MapperIgnoreTarget(nameof(Boat.CreatedAt))]
    public static partial Boat ToEntity(this BoatModel model);

    [MapperIgnoreTarget(nameof(Boat.CreatedAt))]
    public static partial void AssignTo(this BoatDetailModel model, Boat existing);
}
