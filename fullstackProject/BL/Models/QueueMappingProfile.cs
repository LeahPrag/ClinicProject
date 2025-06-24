using DAL.Models;
using AutoMapper;
namespace BL.Models
{
	public class QueueMappingProfile : Profile
	{
		public QueueMappingProfile()
		{
			CreateMap<AvailableQueue, M_AvailableQueue>()
				.ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
				.ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate))
				.ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Doctor.DoctorId))
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Doctor.LastName)); // לפי הצורך – אולי תעשי דחיסה (Projection) של Doctor
		}
	}
}
