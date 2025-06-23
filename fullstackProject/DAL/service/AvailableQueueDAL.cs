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
    public class AvailableQueueDAL : IAvailableQueueDAL
    {
        public DB_Manager db;

        public AvailableQueueDAL(DB_Manager db)
        {
            this.db = db;
        }

        public Boolean MakeAnAppointment(string doctorId, string clientId, DateTime dateTime)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.
                FirstOrDefault(t => t.Doctor.IdNumber == doctorId &&
                                                           t.AppointmentDate == dateTime);
            ClinicQueue newQueue = new ClinicQueue(c, db.Clients.FirstOrDefault(c => c.IdNumber == clientId));
            if (c == null)
                return false;
            return true;
        }
        //fgufu
        public async Task<bool> MakeAnAppointment(int doctorId, Client client, DateTime dateTime)
        {
            var queue = await db.AvailableQueues
             .FirstOrDefaultAsync(t =>
                     t.DoctorId == doctorId &&
                     t.AppointmentDate.Year == dateTime.Year &&
                     t.AppointmentDate.Month == dateTime.Month &&
                     t.AppointmentDate.Day == dateTime.Day &&
                     t.AppointmentDate.Hour == dateTime.Hour &&
                     t.AppointmentDate.Minute == dateTime.Minute
                  );

            if (queue==null)
            {
                return false;
            }

            db.AvailableQueues.Remove(queue);
            ClinicQueue s = new ClinicQueue();
            s.DoctorId = queue.DoctorId;
            s.AppointmentDate = queue.AppointmentDate;
            s.ClientId = client.ClientId;
            s.Client = client;
            db.ClinicQueues.Add(s);
            //await db.SaveChangesAsync();
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("‼️ SaveChangesAsync failed");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message); // זה מה שחשוב באמת!
                throw;
            }
            return true;
        }
        public Boolean MakeAnAppointment(DateTime dateTime)
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

        public async Task<List<AvailableQueue>> GetDoctorAvailableQueueForASpesificDay(int doctorId, DateOnly day)
        {
            return await db.AvailableQueues.Where(t => t.DoctorId == doctorId &&
                                                     t.AppointmentDate.Date == day.ToDateTime(TimeOnly.MinValue).Date)
                                                     .ToListAsync();

        }
        public async Task<List<AvailableQueue>> GetAvailableQueueForASpesificDay( DateOnly day)
        {
            return await db.AvailableQueues.Include(q => q.Doctor).Where(t => t.AppointmentDate.Date == day.ToDateTime(TimeOnly.MinValue).Date)
                                                     .ToListAsync();
        }
        public async Task<List<AvailableQueue>> AvailableQueuesForASpezesilation(string specialization)
        {
            return await db.AvailableQueues
                        .Include(q => q.Doctor) 
                        .Where(q => q.Doctor.Specialization == specialization)
                        .ToListAsync();
        }
        public async Task<AvailableQueue> GetDoctorAvailableQueueForASpecificHour(int doctorId, DateTime appointmentDate)
        {
            return await db.AvailableQueues
                .FirstOrDefaultAsync(t => t.DoctorId == doctorId && t.AppointmentDate == appointmentDate);
        }
        public async Task<bool> AddAvailableQueue(AvailableQueue availableQueue)
        {
            db.Add(availableQueue);
            await db.SaveChangesAsync();
            return true;
        }


    }

}

