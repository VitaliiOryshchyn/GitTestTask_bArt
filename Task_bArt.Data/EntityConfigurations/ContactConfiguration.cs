using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_bArt.Data.Entity;

namespace Task_bArt.Data.EntityConfigurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Email).IsUnique();

            builder
                .HasOne(x => x.Account)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.AccountId)
                .IsRequired();


        }
    }
}
