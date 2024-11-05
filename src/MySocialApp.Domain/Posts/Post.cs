namespace MySocialApp.Domain;

public class Post : Entity
{
    public string Content { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }
    public IReadOnlyCollection<PostLike> Likes { get; }

    public static Post Create(string content , string userId)
    {
        return new Post
        {
            Content = content,
            UserId = userId
        };
    }

}
