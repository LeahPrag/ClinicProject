using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    internal class DoctorDAL
    {
        private readonly DB_Manager _dbManager;
        public DoctorDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        public List<Doctor> GetList()
        {
            return _dbManager.Doctors.ToList();
        }

        public List<ClinicQueue> GetClassId(int doctorId, int day)
        {

            var clinicQueues = _dbManager.ClinicQueues
                .Where(t => t.DoctorId == doctorId && t.AppointmentDate.Day == day)
                .ToList(); 

            return clinicQueues;
        }
        public List<int> ClientsNamse(int doctorID)
        {
            return _dbManager.ClinicQueues
                             .Where(c => c.DoctorId== doctorID)
                             .Select(c => c.ClientId)
                             .ToList();
        }
    }
}
