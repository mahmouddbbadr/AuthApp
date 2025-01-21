using AuthApp.Application.AuthRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Presentation.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Checks if the user is authorized.
        /// </summary>
        /// <remarks>
        /// This endpoint requires general authorization. 
        /// Users must be logged in to access this endpoint.
        /// </remarks>
        /// <returns>Returns a message indicating the user is authorized.</returns>
        [Authorize]
        [HttpGet("generalAuth")]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok("You are Authorized");
        }

        /// <summary>
        /// Checks if the user is authorizes and has the "User" role.
        /// </summary>
        /// <remarks>
        /// This endpoint is restricted to users with the role "User". 
        /// Use this to verify role-based access.
        /// </remarks>
        /// <returns>Returns a message indicating the user has the "User" role.</returns>
        [Authorize(Roles =AuthRoles.User)]
        [HttpGet("userAuth")]
        public async Task<IActionResult> CheckAuthUser()
        {
            return Ok("You are authorized user");
        }

        /// <summary>
        /// Checks if the user is authorizes and has the "Admin" role.
        /// </summary>
        /// <remarks>
        /// This endpoint is restricted to users with the role "Admin". 
        /// Use this to verify admin-level access.
        /// </remarks>
        /// <returns>Returns a message indicating the user has the "Admin" role.</returns>
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpGet("adminAuth")]
        public async Task<IActionResult> CheckAuthAdmin()
        {
            return Ok("You are authorized admin");
        }
    }
}
