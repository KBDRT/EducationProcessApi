using Application.CQRS.Auth.Commands.CreateRole;
using Application.CQRS.Auth.Commands.RegisterUser;
using AutoMapper;
using Presentation.Contracts.Auth;

namespace Presentation.Mapping
{
    public class MappingProfileRequest : Profile
    {
        public MappingProfileRequest()
        {
            CreateMap<RegisterRequest, RegisterUserCommand>();
        }
    }
}
