using Application.CQRS.Auth.Commands.CreateRole;
using Application.CQRS.Auth.Commands.RegisterUser;
using Application.DTO.Auth;
using AutoMapper;
using Domain.Entities.Auth;


namespace Application.Mapping
{
    public class MappingProfileDto : Profile
    {
        public MappingProfileDto()
        {
            CreateMap<CreateRoleCommand, Role>();
            CreateMap<RegisterUserCommand, User>();

            CreateMap<Role, RoleDto>()
             .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.RoleNameRu, opt => opt.MapFrom(src => src.NameRu))
             .ForMember(dest => dest.RoleDescription, opt => opt.MapFrom(src => src.Description));

            CreateMap<User, UserWithRoleDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => src.Initials.Initials))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
        }
    }
}
