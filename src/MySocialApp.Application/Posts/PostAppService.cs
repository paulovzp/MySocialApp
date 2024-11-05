using MySocialApp.Domain;
using MySocialApp.Infrastructure.Exceptions;

namespace MySocialApp.Application;

internal class PostAppService : IPostAppService
{
    private readonly IUserSession _userSession;
    private readonly IPostService _postService;
    private readonly IPostRepository _postRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PostAppService(IUserSession userSession,
        IPostService postService,
        IPostRepository postRepository,
        IUnitOfWork unitOfWork)
    {
        _userSession = userSession;
        _postService = postService;
        _postRepository = postRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginationResponse<PostFeedResponse>> GetFeed(FilterPaginatedRequest filterRequest)
    {
        var posts = await _postRepository.GetFeed(_userSession.Id, filterRequest.Page, filterRequest.PageSize);
        return new PaginationResponse<PostFeedResponse>( posts.Item1.Select(p => new PostFeedResponse
        {
            Id = p.Id,
            Content = p.Content,
            Likes = p.LikesCount,
            Liked = p.Liked,
            CreateAt = p.CreateAt
        }), posts.Item2);
    }

    public async Task Add(PostRequest request)
    {
        var post = Post.Create(request.Content, _userSession.Id);
        await _postService.Add(post);
        await _unitOfWork.Commit();
    }

    public async Task Update(Guid postId, PostRequest request)
    {
        var post = await GetUserPost(postId);
        await _postService.Update(post);
        await _unitOfWork.Commit();
    }

    public async Task LikePost(Guid postId)
    {
        var post = await GetUserPost(postId);
        var likePost = PostLike.Create(post, _userSession.Id);
        await _postService.Add(likePost);
        await _unitOfWork.Commit();
    }

    public async Task UnlikePost(Guid postId)
    {
        var likePost = await _postRepository.GetPostLikeUser(postId, _userSession.Id);
        if (likePost is null)
            throw new NotFoundException($"Couldn't find like for the user {_userSession.Name}");

        await _postService.Remove(likePost);
        await _unitOfWork.Commit();
    }


    public async Task<Post> GetUserPost(Guid postId)
    {
        var post = await _postRepository.Get(postId, _userSession.Id);
        if (post is null)
            throw new NotFoundException($"Couldn't find the post for the user {_userSession.Name}");

        return post;
    }
}