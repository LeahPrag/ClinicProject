using DAL.API;
using DAL.Models;
using DAL.service;

namespace BL.API
{
    public interface IManagerDAL
    {
     //   public DB_Manager _dbManager { get; init; }
        public IClinicQueueDAL _clinicQueueDAL { get; set; }
        public IDoctorDAL _doctorDAL { get; set; }
        public IClientDAL _clientDAL { get; set; }
        public IAvailableQueueDAL _availableQueueDAL { get; set; }

    }
}