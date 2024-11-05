
namespace MySocialApp.Application;

public class PostFeedResponse
{
    public Guid Id { get; internal set; }
    public string Content { get; internal set; }
    public int Likes { get; internal set; }
    public bool Liked { get; internal set; }
    public DateTimeOffset CreateAt { get; internal set; }
}