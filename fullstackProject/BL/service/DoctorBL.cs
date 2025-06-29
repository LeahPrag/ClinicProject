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
			try
			{
				int doctorId = await _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
				var queues = await _managerDal._doctorDAL.GetDoctorQueuesForASpesificDay(doctorId, day);
				return queues.Count;
			}
			catch (Exception)
			{
				throw new DoctorNotExistException(firstName, lastName);
			}
		}
		public async Task<bool> DeleteADayOfWork(string firstName, string lastName, DateOnly day)
        {
            try { 
            int doctorId = await _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            List<ClinicQueue> queues = await _managerDal._doctorDAL.GetDoctorQueuesForASpesificDay(doctorId, day);
            foreach (var q in queues)
            {
                await _managerDal._clinicQueueDAL.DeleteAnApointment(q);
            }
            return true;
			}
			catch (Exception)
			{
				throw new DoctorNotExistException(firstName, lastName);
			}
		}
		public async Task DeleteADoctor(string id)
		{
			try
			{
				Doctor doctor = await _managerDal._doctorDAL.GetADoctorById(id) ?? throw new DoctorNotExistException(id);
				await _managerDal._doctorDAL.DeleteADoctor(doctor);
			}
			catch (Exception)
			{
				throw new DoctorNotExistException(id);
			}
		}
		public async Task UpdateDoctor(UpdateDoctorDto updatedDoctor)
		{
			try
			{
				Doctor existingDoctor = await _managerDal._doctorDAL.GetADoctorById(updatedDoctor.IdNumber) ?? throw new DoctorNotExistException(updatedDoctor.IdNumber);
				if (!string.IsNullOrWhiteSpace(updatedDoctor.FirstName))
					existingDoctor.FirstName = updatedDoctor.FirstName;
				if (!string.IsNullOrWhiteSpace(updatedDoctor.LastName))
					existingDoctor.LastName = updatedDoctor.LastName;
				if (!string.IsNullOrWhiteSpace(updatedDoctor.Specialization))
					existingDoctor.Specialization = updatedDoctor.Specialization;
				await _managerDal._doctorDAL.UpdateDoctor(existingDoctor);
			}
			catch (Exception)
			{
				throw new DoctorNotExistException(updatedDoctor.IdNumber);
			}
		}
		public async Task<List<M_AvailableQueue>> IsDoctorAvailable(string firstName, string lastName, DateOnly day)
        {
            try { 
            int doctorId =await  _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            var Queues= await _managerDal._availableQueueDAL.GetDoctorAvailableQueueForASpesificDay(doctorId, day);
            return _mapper.Map<List<M_AvailableQueue>>(Queues);
			}
			catch (Exception)
			{
				throw new DoctorNotExistException(firstName, lastName);
			}
		}
        public async Task<List<M_Doctor>> GetDoctors()
        {
            var doctors = await _managerDal._doctorDAL.GetDoctors();
            return _mapper.Map<List<M_Doctor>>(doctors);
        }
		public async Task<List<M_ClinicQueue>> GetDoctorQueuesForToday(string idNumber, DateOnly day)
		{
			try
			{
				int doctorId = await _managerDal._doctorDAL.GetDoctorIdByIdNumber(idNumber);
				var queues = await _managerDal._doctorDAL.GetDoctorQueuesForASpesificDay(doctorId, DateOnly.FromDateTime(DateTime.Now));
				return _mapper.Map<List<M_ClinicQueue>>(queues);
			}
			catch (Exception)
			{
				throw new DoctorNotExistException(idNumber);
			}
		}
		public async Task<List<M_AvailableQueue>> GetDoctorAvailableQueuesForASpesificday(string firstName, string lastName, DateOnly day)
        {
            try { 
            int doctorId = await _managerDal._doctorDAL.SearchADoctor(firstName, lastName);
            var queues = await _managerDal._availableQueueDAL.GetDoctorAvailableQueueForASpesificDay(doctorId,day);
            return _mapper.Map<List<M_AvailableQueue>>(queues);
			}
			catch (Exception)
			{
				throw new DoctorNotExistException(firstName, lastName);
			}
		}
		public async Task<List<M_AvailableQueue>> GetAvailableQueuesForASpesificday(DateOnly day)
		{
			try
			{
				var queues = await _managerDal._availableQueueDAL.GetAvailableQueueForASpesificDay(day);
				return _mapper.Map<List<M_AvailableQueue>>(queues);
			}
			catch (Exception)
			{
				throw new AvailableQueueNotFoundException("general", day.ToDateTime(TimeOnly.MinValue));
			}
		}
		public async Task<List<M_AvailableQueue>> AvailableQueuesForASpezesilation(string specialization)
		{
			try
			{
				if (Enum.TryParse<Specialization>(specialization, true, out var result))
				{
					var queues = await _managerDal._availableQueueDAL.AvailableQueuesForASpezesilation(specialization);
					return _mapper.Map<List<M_AvailableQueue>>(queues);
				}
				else
				{
					throw new SpecializationNotExistException(specialization);
				}
			}
			catch (Exception)
			{
				throw new SpecializationNotExistException(specialization);
			}
		}
		public async Task AddDoctor(Doctor doctor)
		{
			try
			{
				if (await _managerDal._doctorDAL.SearchADoctorById(doctor.IdNumber))
					throw new ClientAlreadyExistException(doctor.IdNumber);
				if (!IsValidInput(doctor.FirstName) || !IsValidInput(doctor.LastName))
					throw new IncompatibleOrIincompleteValuesException();
				if (doctor.IdNumber.Length != 9)
					throw new IncompatibleOrIincompleteValuesException();

				await _managerDal._doctorDAL.AddADoctor(doctor);
			}
			catch (Exception)
			{
				throw new IncompatibleOrIincompleteValuesException();
			}
		}
		public static bool IsValidInput(string input)
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
