using Microsoft.EntityFrameworkCore;
using MySocialApp.Domain;

namespace MySocialApp.Persistence;

internal class UserRepository : IUserRepository
{
    private readonly MySocialAppContext _dbContext;

    public UserRepository(MySocialAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Tuple<IEnumerable<UserFollowQueryDto>, int>> GetFollowers(string id, int page, int pageSize)
    {
        var query = _dbContext.Set<UserFollow>()
            .Include(x => x.FollowingUser)
            .Where(x => x.FollowerUserId == id)
            .Select(x => new UserFollowQueryDto
            {
                Id = x.FollowingUserId,
                Name = x.FollowingUser.Name,
                UserName = x.FollowingUser.UserName,
                Email = x.FollowingUser.Email
            });

        return await GetUserFollow(query, page, pageSize);
    }


    public async Task<Tuple<IEnumerable<UserFollowQueryDto>, int>> GetFollowings(string id, int page, int pageSize)
    {
        var query = _dbContext.Set<UserFollow>()
            .Include(x => x.FollowerUser)
            .Where(x => x.FollowingUserId == id)
            .Select(x => new UserFollowQueryDto
            {
                Id = x.FollowerUserId,
                Name = x.FollowerUser.Name,
                UserName = x.FollowerUser.UserName,
                Email = x.FollowerUser.Email
            });

        return await GetUserFollow(query, page, pageSize);
    }

    private async Task<Tuple<IEnumerable<UserFollowQueryDto>, int>> GetUserFollow(IQueryable<UserFollowQueryDto> query, int page, int pageSize)
    {
        var count = await query.CountAsync();

        if(count == 0)
            return new Tuple<IEnumerable<UserFollowQueryDto>, int>([], count);

        var users = await query
        .OrderBy(x => x.Name)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        return new Tuple<IEnumerable<UserFollowQueryDto>, int>(users, count);
    }
}
