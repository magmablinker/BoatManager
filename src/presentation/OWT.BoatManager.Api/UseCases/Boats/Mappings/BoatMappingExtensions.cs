using OWT.BoatManager.Api.UseCases.Boats.Dtos;
using OWT.BoatManager.Application.UseCases.Boats.Models;
using Riok.Mapperly.Abstractions;

namespace OWT.BoatManager.Api.UseCases.Boats.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
internal static partial class BoatMappingExtensions
{
    public static partial BoatModel ToModel(this BoatDto boatDto);
    public static partial BoatDetailModel ToDetailModel(this BoatDto boatDto, Guid id);
}
