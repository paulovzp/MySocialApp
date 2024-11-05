using Microsoft.EntityFrameworkCore;
using MySocialApp.Domain;

namespace MySocialApp.Persistence;

internal class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(MySocialAppContext dbContext)
        : base(dbContext)
    {
    }

    public async Task Add(PostLike likePost)
    {
        await _dbContext.Set<PostLike>().AddAsync(likePost);
    }

    public async Task<Post> Get(Guid postId, string userId)
    {
        return await _dbSet.
            FirstOrDefaultAsync(x => x.Id == postId && x.UserId == userId);
    }

    public async Task<Tuple<IEnumerable<FeedQueryDto>, int>> GetFeed(string userId, int page, int pageSize)
    {
        var query = _dbSet
            .Include(x => x.Likes)
            .Where(x => x.UserId == userId)
            .Select(x => new FeedQueryDto
            {
                Id = x.Id,
                UserName = x.User.Name,
                CreateAt = x.CreateAt,
                Content = x.Content,
                LikesCount = x.Likes.Count(),
                Liked = x.Likes.Any(l => l.UserId == userId)
            });

        var queryFollowing = _dbContext.Set<UserFollow>()
            .Include(x => x.FollowingUser)
            .ThenInclude(x => x.Posts)
            .Where(x => x.FollowerUserId == userId)
            .SelectMany(x => x.FollowingUser.Posts)
            .Select(x => new FeedQueryDto
            {
                Id = x.Id,
                UserName = x.User.Name,
                CreateAt = x.CreateAt,
                Content = x.Content,
                LikesCount = x.Likes.Count(),
                Liked = x.Likes.Any(l => l.UserId == userId)
            });

        query = query.Union(queryFollowing);

        var count = await query.CountAsync();

        if (count == 0)
            return new Tuple<IEnumerable<FeedQueryDto>, int>([], 0);

        var posts = await query
            .OrderByDescending(x => x.CreateAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new Tuple<IEnumerable<FeedQueryDto>, int>(posts, count);
    }

    public async Task<PostLike> GetPostLikeUser(Guid postId, string userId)
    {
        return await _dbContext.Set<PostLike>()
             .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);
    }

    public async Task Remove(PostLike likePost)
    {
        await Task.FromResult(_dbContext.Set<PostLike>().Remove(likePost));
    }

    public async Task<bool> UserLikedPost(Guid postId, string userId)
    {
        return await _dbContext.Set<PostLike>()
        .AnyAsync(x => x.PostId == postId && x.UserId == userId);
    }
}
