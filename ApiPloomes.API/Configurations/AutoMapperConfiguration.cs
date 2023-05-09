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

            CreateMap<CategoriaRequest, Categoria>().ReverseMap();
            CreateMap<Categoria, CategoriaResponse>().ReverseMap();

            CreateMap<ProdutoRequest, Produto>();
            CreateMap<Produto, ProdutoResponse>()
                .ForMember(dest => dest.Usuario, opts => opts.MapFrom(m => m.Usuario))
                .ForMember(p => p.Categoria, opts => opts.MapFrom(m => m.Categoria));

            //CreateMap<List<Produto>, List<ProdutoResponse>>();

        }
    }
}
