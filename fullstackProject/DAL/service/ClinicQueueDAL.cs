using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<bool> DeleteAnApointment(string doctorID, string clientID, DateTime date)
        {
            var queue = await _dbManager.ClinicQueues
                .FirstOrDefaultAsync(q => q.Client.IdNumber == clientID && q.Doctor.IdNumber == doctorID
                && q.AppointmentDate==date );
            if (queue == null)
            {
                return false;
            }
            _dbManager.ClinicQueues.Remove(queue);
            await _dbManager.SaveChangesAsync(); 
            return true;
        }
        public async Task<bool> DeleteAnApointment(ClinicQueue queue)
        {
            if (queue == null)
            {
                return false;
            }
            _dbManager.ClinicQueues.Remove(queue);
            await _dbManager.SaveChangesAsync();
            return true;
        }
        public async Task<List<int>> ClientsNames(int doctorID)
        {
            return await _dbManager.ClinicQueues
                .Where(c => c.DoctorId == doctorID)
                .Select(c => c.ClientId)
                .ToListAsync();
        }
    }
}
