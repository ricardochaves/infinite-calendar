using AutoMapper;

using InfiniteCalendar.Models.DTOs;

namespace InfiniteCalendar.Models.Mapper
{


    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Holiday, HolidayDto>().ReverseMap();
        }
    }
}
