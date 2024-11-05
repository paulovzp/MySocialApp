using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MySocialApp.Domain;

public class User : IdentityUser
{
    public string Name { get; set; }
    public IReadOnlyCollection<Post> Posts { get; set; }
    public IReadOnlyCollection<PostLike> Likes { get; set; }
    public IReadOnlyCollection<UserFollow> Followings { get; set; }
    public IReadOnlyCollection<UserFollow> Followers { get; set; }

    public static class PropertyLength
    {
        public static int Name => 150;
    }
}
