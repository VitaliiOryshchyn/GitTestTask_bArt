using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_bArt.Data.Entity;

namespace Task_bArt.Data.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired();

            builder
                .HasOne(x => x.Incident)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.IncidentName)
                .IsRequired();

        }
    }
}
