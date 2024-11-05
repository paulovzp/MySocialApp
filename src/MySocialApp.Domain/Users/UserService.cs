


namespace MySocialApp.Domain;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Follow(string userFollowingId, string userFollowerId)
    {
        throw new NotImplementedException();
    }

    public async Task UnFollow(string userFollowingId, string userFollowerId)
    {
        throw new NotImplementedException();
    }
}