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
    public class ManagerDAL:IManagerDAL
    {
        private readonly IClinicQueueDAL _clinicQueueDAL;

        private readonly IDoctorDAL _doctorDAL;
        private readonly IClientDAL _clientDAL;
        public ManagerDAL(IClinicQueueDAL clinicQueueDAL, IDoctorDAL doctorDAL)
        {
            _clinicQueueDAL= clinicQueueDAL;
            _doctorDAL= doctorDAL;
        }

        public IClinicQueueDAL GetClinicQueueDAL()
        {
            return _clinicQueueDAL;
        }
        public IDoctorDAL GetDoctorDAL()
        {
            return _doctorDAL;
        }
        public IClientDAL GetClientDAL()
        {
            return _clientDAL;
        }

        
    }
}
