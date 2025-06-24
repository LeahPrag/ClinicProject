using AutoMapper;
namespace BL
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<DAL.Models.Client, BL.Models.M_Client>().ReverseMap();
            CreateMap<DAL.Models.Doctor, BL.Models.M_Doctor>().ReverseMap();
            CreateMap<DAL.Models.AvailableQueue, BL.Models.M_AvailableQueue>().ReverseMap();
        }
    }
}