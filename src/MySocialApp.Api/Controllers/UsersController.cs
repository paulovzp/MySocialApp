using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySocialApp.Application;

namespace MySocialApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController: ControllerBase
{
    private readonly IUserAppService _appService;

    public UsersController(IUserAppService appService)
    {
        _appService = appService;
    }

    [HttpGet("followings")]
    [ProducesResponseType(typeof(PaginationResponse<UserFollowResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFollowings([FromQuery] FilterPaginatedRequest request)
    {
        return Ok(await _appService.GetFollowings(request));
    }


    [HttpGet("followers")]
    [ProducesResponseType(typeof(PaginationResponse<UserFollowResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFollowers([FromQuery] FilterPaginatedRequest request)
    {
        return Ok(await _appService.GetFollowers(request));
    }

    [HttpPost("{userId}/follow")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Follow([FromRoute] string userId)
    {
        await _appService.Follow(userId);
        return NoContent();
    }

    [HttpPost("{userId}/remove/follow")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnFollow([FromRoute] string userId)
    {
        await _appService.UnFollow(userId);
        return NoContent();
    }
}
