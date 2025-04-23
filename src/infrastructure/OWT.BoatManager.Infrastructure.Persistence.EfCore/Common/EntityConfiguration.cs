using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OWT.BoatManager.Core.Common;

namespace OWT.BoatManager.Infrastructure.Persistence.EfCore.Common;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    void IEntityTypeConfiguration<TEntity>.Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(nc => nc.Id);

        builder.Property(p => p.Id)
            .HasDefaultValueSql("NEWID()");

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        Configure(builder);
    }

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}