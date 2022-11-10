using Intsoft.Exam.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Intsoft.Exam.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.FirstName).HasMaxLength(50);
            builder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.NationalCode).HasMaxLength(10).IsRequired();
            builder.Property(a => a.PhoneNumber).HasMaxLength(15).IsRequired();

            builder
                .HasIndex(p => new { p.NationalCode }).IsUnique();
        }
    }
}
