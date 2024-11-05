
namespace MySocialApp.Domain;

public interface IPostService : IService<Post>
{
    Task Add(PostLike likePost);
    Task Remove(PostLike likePost);
}
