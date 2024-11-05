using MySocialApp.Domain;

namespace MySocialApp.Application;

internal class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IUserSession _userSession;
    private readonly IUnitOfWork _unitOfWork;

    public UserAppService(IUserRepository userRepository,
        IUserService userService,
        IUserSession userSession,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userService = userService;
        _userSession = userSession;
        _unitOfWork = unitOfWork;
    }

    public async Task Follow(string userFollowingId)
    {
        await _userService.Follow(userFollowingId, _userSession.Id);
        await _unitOfWork.Commit();
    }

    public async Task UnFollow(string userFollowingId)
    {
        await _userService.UnFollow(userFollowingId, _userSession.Id);
        await _unitOfWork.Commit();
    }

    public async Task<PaginationResponse<UserFollowResponse>> GetFollowers(FilterPaginatedRequest request)
    {
        var users = await _userRepository.GetFollowers(_userSession.Id, request.Page, request.PageSize);
        return new PaginationResponse<UserFollowResponse>(users.Item1.Select(x => new UserFollowResponse
        {
            Id = x.Id,
            Name = x.Name,
            UserName = x.UserName,
            Email = x.Email
        }), users.Item2);
    }

    public async Task<PaginationResponse<UserFollowResponse>> GetFollowings(FilterPaginatedRequest request)
    {
        var users = await _userRepository.GetFollowings(_userSession.Id, request.Page, request.PageSize);
        return new PaginationResponse<UserFollowResponse>(users.Item1.Select(x => new UserFollowResponse
        {
            Id = x.Id,
            Name = x.Name,
            UserName = x.UserName,
            Email = x.Email
        }), users.Item2);
    }


}
