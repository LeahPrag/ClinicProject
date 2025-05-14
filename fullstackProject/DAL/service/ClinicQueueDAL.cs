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
    public class ClinicQueueDAL:IClinicQueueDAL
    {
        private readonly DB_Manager _dbManager;
        public ClinicQueueDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<List<ClinicQueue>> GetList()
        {
            return await _dbManager.ClinicQueues.ToListAsync();
        }
        public async Task<bool> DeleteAnApointment(int doctorID, int clientID)
        {
            var queue = await _dbManager.ClinicQueues
                .FirstOrDefaultAsync(q => q.ClientId == clientID && q.DoctorId == doctorID);

            if (queue == null)
            {
                return false;
            }

            _dbManager.ClinicQueues.Remove(queue);
            await _dbManager.SaveChangesAsync(); 
            return true;
        }

        public async Task<List<ClinicQueue>> GetDoctorQueuesForToday(int doctorId, DateOnly day)
        {

            return await _dbManager.ClinicQueues
                .Where(t => t.DoctorId == doctorId && t.AppointmentDate.Day == day.Day && t.AppointmentDate.Month == day.Month && t.AppointmentDate.Year == day.Year)
                .ToListAsync();


        }
        public async Task<List<int>> ClientsNames(int doctorID)
        {
            return await _dbManager.ClinicQueues
                .Where(c => c.DoctorId == doctorID)
                .Select(c => c.ClientId)
                .ToListAsync();
        }
        //public int SearchADoctor(string doctor_firtsname, string doctor_lastname)
        //{

        //    List<Doctor> doctors = _dbManager.Doctors.ToList();
        //    Doctor d = doctors.FirstOrDefault(x => x.FirstName.Equals(doctor_firtsname) && x.FirstName.Equals(doctor_lastname));
        //    if (d == null)
        //    {
        //        return d.DoctorId;
        //    }
        //    return -1;

        //}

    }
}
