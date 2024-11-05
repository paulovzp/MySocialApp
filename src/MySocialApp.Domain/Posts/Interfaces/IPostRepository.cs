
using System.Linq.Expressions;

namespace MySocialApp.Domain;

public interface IPostRepository : IRepository<Post>
{
    Task<Post> Get(Guid postId, string userId);
    Task Add(PostLike likePost);
    Task<PostLike> GetPostLikeUser(Guid postId, string userId);
    Task Remove(PostLike likePost);
    Task<Tuple<IEnumerable<FeedQueryDto>, int>> GetFeed(string userId, int page, int pageSize);
    Task<bool> UserLikedPost(Guid postId, string userId);
}