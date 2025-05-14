using BL.API;
using DAL.Models;





namespace BL.service
{
    public class DoctorBL : IDoctorBL
    {

        private readonly IManagerDAL _managerDal;

        public DoctorBL(IManagerDAL managerDal)
        {
            _managerDal = managerDal;
        }

        public int GetNumOfClientForToday(string firstName, string lastName, DateOnly day)
        {

            int doctorId = _managerDal._doctorDAL.SearchADoctor(firstName, lastName).Result;
            return _managerDal._doctorDAL.GetDoctorQueesForToday(doctorId, day).Result;
        }
        public void DeleteADayOfWork(string firstName, string lastName, DateOnly day)
        {
            int doctorId = _managerDal._doctorDAL.SearchADoctor(firstName, lastName).Result;
            List<ClinicQueue> queues =  _managerDal._clinicQueueDAL.GetDoctorQueuesForToday(doctorId, day).Result;
            foreach (var q in queues)
            {
                _managerDal._dbManager.ClinicQueues.Remove(q);
            }
             _managerDal._dbManager.SaveChangesAsync();



        }
        public List<AvailableQueue> IsDoctorAvailable(string firstName, string lastName, DateOnly day)
        {
            int doctorId =  _managerDal._doctorDAL.SearchADoctor(firstName, lastName).Result;
            return  _managerDal._availableQueueDAL.GetDoctorAvailableQueueForASpesificDay(doctorId, day).Result;
        }

    }
}
