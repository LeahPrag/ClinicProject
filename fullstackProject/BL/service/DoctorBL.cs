using BL.API;
using DAL.Models;
using DAL.service;
using AutoMapper;
using BL.Models;






namespace BL.service
{
    public class DoctorBL : IDoctorBL
    {

        private readonly IManagerDAL _managerDal;
        private readonly IMapper _mapper;

        public DoctorBL(IManagerDAL managerDal, IMapper mapper)
        {
            _managerDal = managerDal;
            _mapper = mapper;

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

        public async Task<List<M_Doctor>> GetDoctors()
        {
            var doctors = await _managerDal._doctorDAL.GetDoctors();
            return _mapper.Map<List<M_Doctor>>(doctors);
        }

    }
}
