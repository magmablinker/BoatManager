using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OWT.BoatManager.Core.UseCases.Boats;
using OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.UseCases.Boats;
internal sealed class BoatEntityTypeConfiguration : EntityConfiguration<Boat>
{
    public override void Configure(EntityTypeBuilder<Boat> builder)
    {
        builder.Property(boat => boat.Name)
            .HasMaxLength(BoatConstraints.NameMaxLength);

        builder.Property(boat => boat.Description)
            .HasMaxLength(BoatConstraints.DescriptionMaxLength);
    }
}
