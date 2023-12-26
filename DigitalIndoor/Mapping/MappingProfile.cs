using AutoMapper;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Models;
using DigitalIndoor.Models.Common;
using DigitalIndoor.Models.Options;

namespace DigitalIndoor.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Dto to Model

            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<RoleCreateDto, Role>();

            // Model to Dto

            CreateMap<User, UserViewDto>()
                    .ForMember(x => x.RoleName, y => y.MapFrom(c => c.Role.Name));

            CreateMap<User, UserInfoDto>()
                    .ForMember(x => x.RoleName, y => y.MapFrom(c => c.Role.Name))
                    .ForMember(x => x.Functionals, y => y.MapFrom(c => c.Role.Functionals));

            CreateMap<User, BaseUser>();
            CreateMap<Role, RoleViewDto>();
            CreateMap<Video, VideoViewDto>()
                .ForMember(x => x.UserFullName, y => y.MapFrom(c => c.User.FullName));


           

        }
    }
}
