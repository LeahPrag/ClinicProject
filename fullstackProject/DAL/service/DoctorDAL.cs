using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace DAL.service
{
    public class DoctorDAL : IDoctorDAL
    {
        private DB_Manager _dbManager;
        public DoctorDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        public async Task<List<ClinicQueue>> GetDoctorQueuesForASpesificDay(string doctorId, DateOnly day)
        {
            var dateToCheck = day.ToDateTime(TimeOnly.MinValue).Date;
            int number = int.Parse(doctorId);
            List<ClinicQueue> clinicQueues = await _dbManager.ClinicQueues
                .Include(q => q.Client)
                .Include(q => q.Doctor)
                .Where(q => q.DoctorId == number &&
                    q.AppointmentDate.Date == dateToCheck)
                .ToListAsync();
            return clinicQueues ?? throw new Exception("There is no queues for this docror in this date");
        }
        public async Task<int> SearchADoctor(string doctor_firtsname, string doctor_lastname)
        {
            try
            {
                int? id = await _dbManager.Doctors
                    .Where(c => c.FirstName == doctor_firtsname && c.LastName == doctor_lastname)
                    .Select(c => (int?)c.DoctorId)
                    .FirstOrDefaultAsync();
                return id ?? throw new Exception("The doctor is not exist");
            }
            catch (Exception ex)
            {
                throw new Exception($"DAL Error - SearchADoctor: {ex.Message}", ex);
            }
        }
        public async Task<bool> SearchADoctorById(string id)
        {
            var doctor = await _dbManager.Doctors.FirstOrDefaultAsync(c => c.IdNumber == id);
            return doctor != null;
        }
        public async Task<Doctor> GetADoctorById(string id)
        {
            try
            {
                Doctor? d = await _dbManager.Doctors.FirstOrDefaultAsync(d => d.IdNumber == id);
                return d ?? throw new Exception($"Error while getting doctor by ID: {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> GetDoctorIdByIdNumber(string id)
        {
            try
            {
                Doctor? doctor = await _dbManager.Doctors.FirstOrDefaultAsync(d => d.IdNumber == id);
                if (doctor == null)
                    throw new Exception($"Error while getting doctorId by ID: {id}");
                return doctor.DoctorId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateDoctor(Doctor doctor)
        {
            await _dbManager.SaveChangesAsync();
        }
        public async Task<List<Doctor>> GetDoctors()
        {
            return await _dbManager.Doctors.ToListAsync();
        }
        public async Task AddADoctor(Doctor doctor)
        {
            await _dbManager.Doctors.AddAsync(doctor);
            await _dbManager.SaveChangesAsync();
        }
        public async Task<List<Doctor>> GetDoctorsWithDays()
        {
            return await _dbManager.Doctors
                .Include(d => d.DayDoctors)
                    .ThenInclude(dd => dd.Day)
                .ToListAsync();
        }
        public async Task DeleteADoctor(Doctor doctor)
        {
            _dbManager.Doctors.Remove(doctor);
            await _dbManager.SaveChangesAsync();
        }
    }
}
