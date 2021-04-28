using JwtApp.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {

        [Authorize]
        [HttpGet("welcome")]
        public Result<string> Welcome()
        {
            return Result<string>.Success("Welcome");
        }



    }
}