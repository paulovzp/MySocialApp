using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySocialApp.Domain;

namespace MySocialApp.Persistence;

public class PostMappping : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.CreateAt)
            .IsRequired()
            .HasMaxLength(int.MaxValue);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.CreateAt)
            .IsRequired();

        builder.Property(x => x.UpdateAt)
            .IsRequired();
    }
}