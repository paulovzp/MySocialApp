using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MySocialApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {

        public HealthCheckController()
        {

        }

        [HttpGet]
        public IActionResult Get() => NoContent();

        [HttpGet("logged")]
        [Authorize]
        public IActionResult GetLogged() => NoContent();

    }
}
