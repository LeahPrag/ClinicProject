using DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    internal class ApointmentManager
    {
        public DB_Manager db;
        public Boolean MakeAnApointment(string doctor_firtsname, string doctor_lastname, string client_firstname, string client_lastname)
        {
            List<Client> clients = db.Clients.ToList();
            int c=clients.FirstOrDefault( x => x.FirstName.Equals(client_firstname) && x.LastName.Equals(client_lastname)).ClientId;
            List<Doctor> doctors = db.Doctors.ToList();
            int d = doctors.FirstOrDefault(x => x.FirstName.Equals(doctor_firtsname) && x.LastName.Equals(doctor_lastname)).DoctorId;
            if (d==null || c==null )
            {
                return false;
            }

            return true;
        }
        public DateTime CheckFirstApoitmentAvailable()
        {
            foreach (Doctor d in db.Doctors)
            {

            }
        }
    }
}
