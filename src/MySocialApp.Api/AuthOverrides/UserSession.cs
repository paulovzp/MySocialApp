using MySocialApp.Application;
using System.Security.Claims;

namespace MySocialApp.Api.AuthOverrides;

public class UserSession : IUserSession
{

    private readonly IHttpContextAccessor _context;

    public UserSession(IHttpContextAccessor context)
    {
        _context = context;
    }

    public string AuthenticatedUserToken
    {
        get
        {
            return _context.HttpContext.Request.Headers["Authorization"];
        }
    }

    public string Id
    {
        get
        {
            string userId = GetClaimValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }

    public string Name
    {
        get
        {
            string name = GetClaimValue(ClaimTypes.Name);
            return string.IsNullOrEmpty(name) ? string.Empty : name;
        }
    }

    public string Email
    {
        get
        {
            string email = GetClaimValue(ClaimTypes.Email);
            return string.IsNullOrEmpty(email) ? string.Empty : email;
        }
    }

    private string GetClaimValue(string claimType)
    {
        return _context.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }

}
