using AutoMapper;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.Models.Options;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;

namespace DigitalIndoorAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Dto to Model

            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<RoleCreateDto, Role>();

            CreateMap<PlayListCreateDto, PlayList>();

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

            CreateMap<PlayList, PlayListViewDto>()
                .ForMember(x => x.UserFullName, y => y.MapFrom(c => c.User.FullName));


           

        }
    }
}
