using BL.API;
using DAL.Models;
using DAL.service;





namespace BL.service
{
    public class DoctorBL : IDoctorBL
    {

        private readonly IManagerDAL _managerDal;

        public DoctorBL(IManagerDAL managerDal)
        {
            _managerDal = managerDal;
        }

        public async Task<int> GetNumOfClientForToday(string firstName, string lastName, DateOnly day)
        {

            int doctorId =await _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            return await _managerDal._doctorDAL.GetDoctorQueesForToday(doctorId, day);
        }
        public async Task DeleteADayOfWork(string firstName, string lastName, DateOnly day)
        {
            int doctorId =await  _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            List<ClinicQueue> queues = await _managerDal._clinicQueueDAL.GetDoctorQueuesForToday(doctorId, day);
            foreach (var q in queues)
            {
                _managerDal._dbManager.ClinicQueues.Remove(q);
            }
            await _managerDal._dbManager.SaveChangesAsync();



        }
        public async Task<List<AvailableQueue>> IsDoctorAvailable(string firstName, string lastName, DateOnly day)
        {
            int doctorId =await  _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            return await _managerDal._availableQueueDAL.GetDoctorAvailableQueueForASpesificDay(doctorId, day);
        }

        public async Task<List<Doctor>> GetDoctors()
        {
            return await _managerDal._doctorDAL.GetDoctors();
        }

    }
}
