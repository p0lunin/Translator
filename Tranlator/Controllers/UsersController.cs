using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tranlator.Filters;
using Tranlator.Services;
using Tranlator.ViewModels.Errors;

namespace Tranlator.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route(ApiConstant.Prefix + "[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ApiError), 401)]
        public async Task<IActionResult> SendAuthLink([FromQuery] string email)
        {
            await _userService.SendAuthLink(email);
            return new JsonResult("ok");
        }
    }
}