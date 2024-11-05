
using MySocialApp.Infrastructure.Exceptions;

namespace MySocialApp.Domain;

internal class PostService : Service<Post>, IPostService
{
    private IPostRepository PostRepository => (IPostRepository)_repository;

    public PostService(IPostRepository repository) 
        : base(repository)
    {
    }

    public async Task Add(PostLike likePost)
    {
        if (await PostRepository.UserLikedPost(likePost.PostId, likePost.UserId))
            throw new DomainException("User already liked this post");

        await PostRepository.Add(likePost);
    }

    public async Task Remove(PostLike likePost)
    {
        await PostRepository.Remove(likePost);
    }
}
