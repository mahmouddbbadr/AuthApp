using AuthApp.Application.DTOs;
using AuthApp.Application.Response;


namespace AuthApp.Application.Interfaces
{
    public interface IAppUserService
    {
        Task<Responses> Register(AppUserRegisterDTO appUserRegisterDTO, string role);
        Task<Responses> Login(AppUserLoginDTO appUserLoginDTO);
    }
}
