using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_bArt.Data.Entity;

namespace Task_bArt.Data.EntityConfigurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.HasKey(x => x.IncidentName);
            builder.Property(x => x.IncidentName).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Description);
        }
    }
}
