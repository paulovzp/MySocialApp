
namespace MySocialApp.Domain;

public interface IUserRepository
{
    Task<Tuple<IEnumerable<UserFollowQueryDto>, int>> GetFollowers(string id, int page, int pageSize);
    Task<Tuple<IEnumerable<UserFollowQueryDto>, int>> GetFollowings(string id, int page, int pageSize);
}
