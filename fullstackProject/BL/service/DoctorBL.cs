using BL.API;
using DAL.Models;
using DAL.service;
using AutoMapper;
using BL.Models;
using BL.Exceptions;






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
            var queues = await _managerDal._doctorDAL.GetDoctorQueesForASpesificDay(doctorId, day);
            return queues.Count;
        }
        public async Task<bool> DeleteADayOfWork(string firstName, string lastName, DateOnly day)
        {
            int doctorId =await  _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            List<ClinicQueue> queues = await _managerDal._doctorDAL.GetDoctorQueesForASpesificDay(doctorId, day);
            foreach (var q in queues)
            {
                _managerDal._dbManager.ClinicQueues.Remove(q);
            }
            await _managerDal._dbManager.SaveChangesAsync();

            return  true;

        }

        public async Task<List<M_AvailableQueue>> IsDoctorAvailable(string firstName, string lastName, DateOnly day)
        {
            int doctorId =await  _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            var qeues= await _managerDal._availableQueueDAL.GetDoctorAvailableQueueForASpesificDay(doctorId, day);
            return _mapper.Map<List<M_AvailableQueue>>(qeues);
        }

        public async Task<List<M_Doctor>> GetDoctors()
        {
            var doctors = await _managerDal._doctorDAL.GetDoctors();
            return _mapper.Map<List<M_Doctor>>(doctors);
        }

        public async Task<List<M_ClinicQueue>> GetDoctorQueesForToday(string firstName, string lastName, DateOnly day)
        {
            int doctorId = await _managerDal._doctorDAL.SearchADoctor(firstName, lastName);

            var queues = await _managerDal._doctorDAL.GetDoctorQueesForASpesificDay(doctorId, DateOnly.FromDateTime(DateTime.Now));
            return _mapper.Map<List<M_ClinicQueue>>(queues);
        }
        public async Task<List<M_AvailableQueue>> GetDoctorAvailableQueesForASpesificday(string firstName, string lastName, DateOnly day)
        {
            int doctorId = await _managerDal._doctorDAL.SearchADoctor(firstName, lastName);

            var queues = await _managerDal._availableQueueDAL.GetDoctorAvailableQueueForASpesificDay(doctorId,day);
            return _mapper.Map<List<M_AvailableQueue>>(queues);
        }
        public async Task<List<M_AvailableQueue>> GetAvailableQueesForASpesificday( DateOnly day)
        {
            var queues = await _managerDal._availableQueueDAL.GetAvailableQueueForASpesificDay( day);
            return _mapper.Map<List<M_AvailableQueue>>(queues);
        }
        public async Task<List<M_AvailableQueue>> AvailableQueuesForASpezesilation(string specialization)
        {

            if (Enum.TryParse<Specialization>(specialization, true, out var result))
            {

                var queues = await _managerDal._availableQueueDAL.AvailableQueuesForASpezesilation(specialization);
                return _mapper.Map<List<M_AvailableQueue>>(queues);
            }
            else
            {
                throw new specializationNotExistException(specialization);
            }

        }
        //DateOnly.FromDateTime(DateTime.Now))
    }
}
