using DAL.API;
using DAL.service;

namespace BL.API
{
    public interface IManagerDAL
    {
        public IClinicQueueDAL _clinicQueueDAL { get; set; }
        public IDoctorDAL _doctorDAL { get; set; }
        public IClientDAL _clientDAL { get; set; }
        public IAvailableQueueDAL _availableQueueDAL { get; set; }
    }
}