using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.service
{
    public class DoctorDAL: IDoctorDAL
    {
        private  DB_Manager _dbManager;
        public DoctorDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        public async Task<List<Doctor>> GetList()
        {
            return await _dbManager.Doctors.ToListAsync();
        }
        public async Task<List<ClinicQueue>> GetDoctorQueuesForASpesificDay(int doctorId, DateOnly day)
        {
            var dateToCheck = day.ToDateTime(TimeOnly.MinValue).Date;// I added it to check
            List<ClinicQueue> clinicQueues =await _dbManager.ClinicQueues
                .Include(q=>q.Client)
                .Include(q=> q.Doctor)
                .Where(q => q.DoctorId == doctorId &&
                    q.AppointmentDate.Date == dateToCheck)
                .ToListAsync();
            return  clinicQueues;
        }
        public async Task<List<int>> ClientsNamse(int doctorID)
        {
            return await _dbManager.ClinicQueues
                         .Where(c => c.DoctorId == doctorID)
                         .Select(c => c.ClientId)
                         .ToListAsync();
        }
        public async Task<int> SearchADoctor(string doctor_firtsname, string doctor_lastname)
        {
            int? id = await _dbManager.Doctors
                                      .Where(c => c.FirstName == doctor_firtsname && c.LastName == doctor_lastname)
                                      .Select(c => c.DoctorId)
                                      .FirstOrDefaultAsync();
            if (id == null)
                throw new Exception("the doctor is not exist");
            return  id.Value;
        }
        public async Task<bool> SearchADoctorById(string id)
        {
            var doctor = await _dbManager.Doctors
                                         .FirstOrDefaultAsync(c => c.IdNumber == id);
            return doctor != null;
        }
        public async Task<Doctor> GetADoctorById(string id)
        {
            Doctor? doctor = await _dbManager.Doctors
                                         .FirstOrDefaultAsync(d => d.IdNumber == id);
            return doctor;
        }
        public async Task<int> GetDoctorIdByIdNumber(string id)
        {
            Doctor? doctor = await _dbManager.Doctors
                                         .FirstOrDefaultAsync(d => d.IdNumber == id);
            if (doctor == null)
                return 0;
            return doctor.DoctorId; 
        }
        public async Task<Day?> GetDoctorDay(string doctor_firtsname, string doctor_lastname, int day)
        {
            return await _dbManager.Doctors
                .Where(d => d.FirstName == doctor_firtsname && d.LastName == doctor_lastname)
                .SelectMany(d => d.DayDoctors)
                .Where(dd => dd.Day.DayNum == day)
                .Select(dd => dd.Day)
                .FirstOrDefaultAsync();
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
