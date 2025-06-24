using BL.API;
using DAL.Models;
using DAL.service;
using AutoMapper;
using BL.Models;
using BL.Exceptions;
using System.Numerics;






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
            int doctorId = await _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            List<ClinicQueue> queues = await _managerDal._doctorDAL.GetDoctorQueesForASpesificDay(doctorId, day);
            foreach (var q in queues)
            {
                await _managerDal._clinicQueueDAL.DeleteAnApointment(q);
            }


            return true;

        }
        public async Task DeleteADoctor(string id)
        {
            Doctor doctor = await _managerDal._doctorDAL.GetADoctorById(id);
                               
            if (doctor == null)
            {
                throw new DoctorNotExsistException(id);
            }
            await _managerDal._doctorDAL.DeleteADoctor(doctor);

        }
        public async Task UpdateDoctor(UpdateDoctorDto updatedDoctor)
        {
            Doctor existingDoctor =await  _managerDal._doctorDAL.GetADoctorById(updatedDoctor.IdNumber);  
            if (existingDoctor == null)
                throw new DoctorNotExsistException(updatedDoctor.IdNumber);
            if (!string.IsNullOrWhiteSpace(updatedDoctor.FirstName))
                existingDoctor.FirstName = updatedDoctor.FirstName;

            if (!string.IsNullOrWhiteSpace(updatedDoctor.LastName))
                existingDoctor.LastName = updatedDoctor.LastName;

            if (!string.IsNullOrWhiteSpace(updatedDoctor.Specialization))
                existingDoctor.Specialization = updatedDoctor.Specialization;


  
            await _managerDal._doctorDAL.UpdateDoctor(existingDoctor);
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
        public async Task AddDoctor(Doctor doctor)
        {
            if (await _managerDal._doctorDAL.SearchADoctorById(doctor.IdNumber))
                throw new ClientAlradyExsistException(doctor.IdNumber);

            if (!IsValidInput(doctor.FirstName) || !IsValidInput(doctor.LastName))
                throw new IncompatibleOrIincompleteValuesException();
            if (doctor.IdNumber.Length != 9)
                throw new IncompatibleOrIincompleteValuesException();
            await _managerDal._doctorDAL.AddADoctor(doctor);
        }
        public bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new IncompatibleOrIincompleteValuesException();
            return true;
        }

        public async Task<bool> SearchDoctorById(string idNumber)
        {
            return await _managerDal._doctorDAL.SearchADoctorById(idNumber);
        }
    }
}
