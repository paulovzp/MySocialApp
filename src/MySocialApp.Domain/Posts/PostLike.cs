namespace MySocialApp.Domain;

public class PostLike : Entity
{
    public Guid PostId { get; private set; }
    public Post Post { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }

    public static PostLike Create(Post post, string id)
    {
        return new PostLike
        {
            PostId = post.Id,
            UserId = id
        };
    }
}
