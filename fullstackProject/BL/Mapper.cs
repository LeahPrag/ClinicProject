using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using DAL.Models;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL
{
    public class Mapper : Profile
    {
        public Mapper()
        {

            // Patient
            CreateMap<DAL.Models.Client, BL.Models.M_Client>().ReverseMap();

            // Therapist
            CreateMap<DAL.Models.Doctor, BL.Models.M_Doctor>().ReverseMap();

            // Appointment

        }
    }
}
