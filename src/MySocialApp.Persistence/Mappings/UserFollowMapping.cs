using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySocialApp.Domain;

namespace MySocialApp.Persistence;

public class UserFollowMapping : IEntityTypeConfiguration<UserFollow>
{
    public void Configure(EntityTypeBuilder<UserFollow> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasOne(x => x.FollowerUser)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.FollowerUserId);

        builder.HasOne(x => x.FollowingUser)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.FollowingUserId);

        builder.Property(x => x.CreateAt)
            .IsRequired();

        builder.Property(x => x.UpdateAt)
            .IsRequired();
    }
}
