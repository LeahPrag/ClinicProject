using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<int> GetDoctorQueesForToday(int doctorId, DateOnly day)
        {
            var clinicQueues = _dbManager.ClinicQueues
                .Where(t => t.DoctorId == doctorId && t.AppointmentDate.Day == day.Day && t.AppointmentDate.Month == day.Month && t.AppointmentDate.Year == day.Year)
                .ToList();

            return await Task.FromResult(clinicQueues.Count);
        }

        public async Task<List<int>> ClientsNamse(int doctorID)
        {
            return await _dbManager.ClinicQueues
                         .Where(c => c.DoctorId == doctorID)
                         .Select(c => c.ClientId)
                         .ToListAsync(); // Use ToListAsync for async database operations
        }
        public async Task<int> SearchADoctor(string doctor_firtsname, string doctor_lastname)
        {

            int? id = await _dbManager.Doctors
                                      .Where(c => c.FirstName == doctor_firtsname && c.LastName == doctor_lastname)
                                      .Select(c => c.DoctorId)
                                      .FirstOrDefaultAsync();
            if (id == null)
            {
                return -1;
            }
            return  id.Value;

        }

    }
}
