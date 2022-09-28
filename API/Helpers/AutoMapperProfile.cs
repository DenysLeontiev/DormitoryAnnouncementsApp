using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, UserDto>()
                                        .ForMember(dest => dest.Announcements,opt => opt.MapFrom(src => src.Announcements))
                                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            
            CreateMap<Announcement, ICollection<AnnouncementDto>>();
            CreateMap<Photo, PhotoDto>();
        }
    }
}