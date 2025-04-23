using OWT.BoatManager.Application.UseCases.Boats.Models;
using OWT.BoatManager.Core.UseCases.Boats;
using Riok.Mapperly.Abstractions;

namespace OWT.BoatManager.Application.UseCases.Boats.Queries.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
internal static partial class BoatMappingExtensions
{
    public static partial IReadOnlyList<BoatDetailModel> ToDetailModels(this IReadOnlyList<Boat> boats);
    public static partial BoatDetailModel ToDetailModel(this Boat boat);
}
