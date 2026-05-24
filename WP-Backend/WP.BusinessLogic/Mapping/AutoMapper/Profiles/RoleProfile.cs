using AutoMapper;
using WP.DataAccess.Entities.Permisions;
using WP.Infrastructure.Dtos.Authentication;

namespace WP.BusinessLogic.Mapping.AutoMapper.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions.Select(p => p.Id)))
                .ForMember(dest => dest.UserTypes, opt => opt.MapFrom(src => src.UserTypes.Select(ut => (int)ut)));
        }
    }
}
