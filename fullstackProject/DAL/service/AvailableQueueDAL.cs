using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    public class AvailableQueueDAL
    {
        public DB_Manager db;

        public AvailableQueueDAL(DB_Manager db)
        {
            this.db = db;
        }


        public Boolean MakeAnAppointment(int doctorId, int clientId, DateTime dateTime)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.FirstOrDefault(t => t.DoctorId == doctorId &&
                                                           t.ClientId == clientId &&
                                                           t.AppointmentDate == dateTime);

            if (c != null)
            {
                // Remove it from AvailableQueues
                db.AvailableQueues.Remove(c);
                
                // Add it to ClinicQueues
                //db.ClinicQueues.Add(c);

                return true;
            }

            return false;
        }
        public Boolean MakeAnAppointment( DateTime dateTime)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.All(t => t.AppointmentDate == dateTime);

            if (c != null)
            {
                // Remove it from AvailableQueues
                //db.AvailableQueues.Remove(c);

                //// Add it to ClinicQueues
                //db.ClinicQueues.Add(c);

                return true;
            }

            return false;
        }
    }
}
