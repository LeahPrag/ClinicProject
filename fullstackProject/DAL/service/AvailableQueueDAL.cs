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


<<<<<<< HEAD
        public Boolean MakeAnAppointment(int doctorId, int clientId, DateTime dateTime)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.FirstOrDefault(t => t.DoctorId == doctorId &&
                                                           t.ClientId == clientId &&
                                                           t.AppointmentDate == dateTime);
=======
        public Boolean MakeAnAppointment(int doctorId, DateTime dateTime,Client client)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.FirstOrDefault(t => t.DoctorId == doctorId && t.AppointmentDate == dateTime);
>>>>>>> 23d7b9c75a15578bd658726647031950b36428d6

            if (c != null)
            {
                // Remove it from AvailableQueues
                db.AvailableQueues.Remove(c);
<<<<<<< HEAD
                
                // Add it to ClinicQueues
=======

                // Add it to ClinicQueues
                ClinicQueue s = new ClinicQueue();
                s.DoctorId =c.DoctorId;
                s.AppointmentDate = c.AppointmentDate;
                s.ClientId= client.ClientId;
                s.Client = client;
                s.QueueId = c.QueueId;
                

                


>>>>>>> 23d7b9c75a15578bd658726647031950b36428d6
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
<<<<<<< HEAD
=======


>>>>>>> 23d7b9c75a15578bd658726647031950b36428d6
    }
}
