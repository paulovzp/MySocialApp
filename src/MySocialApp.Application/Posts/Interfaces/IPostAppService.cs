﻿

namespace MySocialApp.Application;

public interface IPostAppService
{
    Task Add(PostRequest request);
    Task<PaginationResponse<PostFeedResponse>> GetFeed(FilterPaginatedRequest filterRequest);
    Task LikePost(Guid postId);
    Task UnlikePost(Guid postId);
    Task Update(Guid PostId, PostRequest request);
}
