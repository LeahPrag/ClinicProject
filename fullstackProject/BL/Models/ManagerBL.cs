using BL.API;
using DAL.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ManagerBL:IManagerBL
    {
        private readonly IClinicQueueDAL _clinicQueueDAL;

        private readonly IDoctorDAL _doctorDAL;


        public ManagerBL(IClinicQueueDAL clinicQueueDAL, IDoctorDAL doctorDAL)
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
    }
}
