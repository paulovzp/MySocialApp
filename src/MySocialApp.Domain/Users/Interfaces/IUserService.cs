

namespace MySocialApp.Domain;

public interface IUserService
{
    Task Follow(string userFollowingId, string userFollowerId);
    Task UnFollow(string userFollowingId, string userFollowerId);
}
