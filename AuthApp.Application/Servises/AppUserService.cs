using AuthApp.Application.DTOs;
using AuthApp.Application.Interfaces;
using AuthApp.Application.Response;
using AuthApp.Domain.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace AuthApp.Application.Servises
{
    public class AppUserService(UserManager<AppUser> userManager, IConfiguration configuration, IMapper mapper) : IAppUserService
    {
        public async Task<Responses> Login(AppUserLoginDTO appUserLoginDTO)
        {
            var user = await userManager.FindByEmailAsync(appUserLoginDTO.Email);
            if (user != null && !user.IsLocked)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, appUserLoginDTO.Password);
                if (checkPassword)
                {
                    var token = CreateToken.CreateJWTToken(userManager, configuration, user).Result;
                    var mappedUser = mapper.Map<AppUserOutputDTO>(user);
                    var roles = await userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();
                    return new Responses() { flag = true, body = new { token, mappedUser}, message = "Logined successfully" };

                }
                return new Responses() { flag = false, body = null, message = "Password is wrong, try again" };

            }
            return new Responses() { flag = false, body = null, message = "User was not found, try again" };

        }

        public async Task<Responses> Register(AppUserRegisterDTO appUserRegisterDTO, string role)
        {
            var checkUserExists = await userManager.FindByEmailAsync(appUserRegisterDTO.Email);
            if (checkUserExists == null)
            {
                var user = new AppUser()
                {
                    Email = appUserRegisterDTO.Email,
                    UserName = appUserRegisterDTO.Email.Substring(0, appUserRegisterDTO.Email.IndexOf("@")),
                    Address = appUserRegisterDTO.Address,
                    PhoneNumber = appUserRegisterDTO.PhoneNumber,
                    Role = role
                };

                var isCreated = await userManager.CreateAsync(user, appUserRegisterDTO.Password);
                if (isCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                    return (new Responses() { flag = true, body = null, message = "Account was added successfully" });
                }
                return (new Responses() { flag = false, body = null, message = "Something went wrong while adding user, please try agian" });
            }
            return (new Responses() { flag = false, body = null, message = "Email already registered" });

        }
    }
}
