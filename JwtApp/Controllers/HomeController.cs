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


        [Authorize(Roles = "Super Admin")]
        [HttpGet("secret")]
        public Result<string> Secret()
        {
            return Result<string>.Success("Secret");
        }

        [Authorize(Policy = "EditPolicy")]
        [HttpGet("edit")]
        public Result<string> Edit()
        {
            return Result<string>.Success("edit");
        }


    }
}