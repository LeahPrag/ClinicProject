using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Models;
using Microsoft.Data.SqlClient;

namespace BL.Models
{
    public class ClinicQueueMappingProfile : Profile
    {
        public ClinicQueueMappingProfile()
        {
            CreateMap<ClinicQueue, M_ClinicQueue>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.ClientId))
                .ForMember(dest => dest.ClientFirstName, opt => opt.MapFrom(src => src.Client.FirstName))
                .ForMember(dest => dest.ClientLastName, opt => opt.MapFrom(src => src.Client.LastName))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Doctor.DoctorId))
                .ForMember(dest => dest.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
                .ForMember(dest => dest.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.LastName));
        }
    }
}
