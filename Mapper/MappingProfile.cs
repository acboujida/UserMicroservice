using AutoMapper;
using UserMicroservice.DTO;
using UserMicroservice.Models;

namespace UserMicroservice.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
        }
    }
}
