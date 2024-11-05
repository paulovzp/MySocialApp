using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySocialApp.Application;

namespace MySocialApp.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostsController : ControllerBase
{
    private readonly IPostAppService _appService;

    public PostsController(IPostAppService appService)
    {
        _appService = appService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet("feed")]
    [ProducesResponseType(typeof(PaginationResponse<PostFeedResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFeed([FromQuery] FilterPaginatedRequest request)
    {
        return Ok(await _appService.GetFeed(request));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] PostRequest request)
    {
        await _appService.Add(request);
        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromRoute] Guid id, [FromBody] PostRequest request)
    {
        await _appService.Update(id, request);
        return NoContent();
    }

    [HttpPost("{postId}/like")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Like([FromRoute] Guid id)
    {
        await _appService.LikePost(id);
        return NoContent();
    }

    [HttpPost("{postId}/remove/like")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnLike([FromRoute] Guid id)
    {
        await _appService.UnlikePost(id);
        return NoContent();
    }
}
