
using MySocialApp.Domain;

namespace MySocialApp.Application;

public interface IUserAppService
{
    Task Follow(string userId);
    Task<PaginationResponse<UserFollowResponse>> GetFollowers(FilterPaginatedRequest request);
    Task<PaginationResponse<UserFollowResponse>> GetFollowings(FilterPaginatedRequest request);
    Task UnFollow(string userId);
}

