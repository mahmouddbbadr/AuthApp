using AuthApp.Application.AuthRole;
using AuthApp.Application.DTOs;
using AuthApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Presentation.Controllers
{
    /// <summary>
    /// Handles user account management, including registration and login operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAppUserService appUserService) : ControllerBase
    {

        /// <summary>
        /// Registers a new user with the "User" role.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the registration of standard users. 
        /// A valid <see cref="AppUserRegisterDTO"/> must be provided in the request body.
        /// </remarks>
        /// <param name="appUserRegisterDTO">Includes the registration details for the user.</param>
        /// <returns>
        /// A result indicating success or failure of the operation.
        /// If successful, returns a flag of true and message indicating success.
        /// </returns>
        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser(AppUserRegisterDTO appUserRegisterDTO)
        {
            var result = await appUserService.Register(appUserRegisterDTO, AuthRoles.User);
            if (result.flag)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        /// <summary>
        /// Registers a new administrator with the "Admin" role.
        /// </summary>
        /// <remarks>
        /// This endpoint is restricted to users with the "Admin" role.
        /// A valid <see cref="AppUserRegisterDTO"/> must be provided in the request body.
        /// </remarks>
        /// <param name="appUserRegisterDTO">Includes the registration details for the admin.</param>
        /// <returns>
        /// A result indicating success or failure of the operation.
        /// If successful, returns a flag of true and message indicating success .
        /// </returns>
        [Authorize(Roles = AuthRoles.Admin)]
        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin(AppUserRegisterDTO appUserRegisterDTO)
        {
            var result = await appUserService.Register(appUserRegisterDTO, AuthRoles.Admin);
            if (result.flag)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <remarks>
        /// This endpoint allows users to log in with their credentials.
        /// A valid <see cref="AppUserLoginDTO"/> must be provided in the request body.
        /// If successful, a JWT token is returned for authentication.
        /// </remarks>
        /// <param name="appUserLoginDTO">Includes the login credentials for the user.</param>
        /// <returns>
        /// A result indicating success or failure of the login operation.
        /// If successful, returns a JWT token and object of user details.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(AppUserLoginDTO appUserLoginDTO)
        {
            var result = await appUserService.Login(appUserLoginDTO);
            if (result.flag)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
