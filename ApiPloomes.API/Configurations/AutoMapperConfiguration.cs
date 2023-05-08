using ApiPloomes.DOMAIN.DTOs.Request;
using ApiPloomes.DOMAIN.DTOs.Response;
using ApiPloomes.DOMAIN.Models;
using AutoMapper;

namespace ApiPloomes.API.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<UserCreationRequest, User>();
            CreateMap<UserUpdateRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
