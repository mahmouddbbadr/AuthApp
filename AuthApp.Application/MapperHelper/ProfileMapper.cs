using AuthApp.Application.DTOs;
using AuthApp.Domain.Entites;
using AutoMapper;


namespace AuthApp.Application.MapperHelper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<AppUser, AppUserOutputDTO>();
            CreateMap<AppUserOutputDTO, AppUser>();
        }
    }
}
