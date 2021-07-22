using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User");

            builder.HasKey( user => user.Id);

            builder.HasIndex( user => user.Email )
                   .IsUnique();
            
            builder.Property( user => user.Name )
                   .IsRequired()
                   .HasMaxLength(60);

            builder.Property( user => user.Email )
                   .HasMaxLength(100);
        }
    }
}
