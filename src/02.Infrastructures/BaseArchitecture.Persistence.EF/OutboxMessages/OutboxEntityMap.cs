using BaseArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseArchitecture.Persistence.EF.OutboxMessages;

internal class OutboxEntityMap : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.Content).IsRequired();
        builder.Property(e => e.OccurredTime).IsRequired();
        builder.Property(e => e.PublishedTime).IsRequired(false);
        builder.Property(e => e.Error).IsRequired(false);
    }
}