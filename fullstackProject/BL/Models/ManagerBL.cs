using BL.API;
using DAL.API;
using DAL.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ManagerBL : IManagerBL
    {
        private readonly IClinicQueueBL _clinicQueueBL;

        private readonly IDoctorBL _doctorBL;
        public ManagerBL(IClinicQueueBL clinicQueueBL, IDoctorBL doctorBL)
        {
            _clinicQueueBL = clinicQueueBL;
            _doctorBL = doctorBL;
        }

        public IClinicQueueBL GetClinicQueueBL()
        {
            return _clinicQueueBL;
        }
        public IDoctorBL GetDoctorBL()
        {
            return _doctorBL;
        }
        //public IClientDAL GetClientDAL()
        //{
        //    return _clientDAL;
        //}
    }
}
