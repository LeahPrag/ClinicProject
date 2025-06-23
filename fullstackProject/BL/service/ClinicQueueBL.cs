using DAL.API;
using BL.API;
using BL.Exceptions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using DAL.service;
using BL.Models;

namespace BL.service
{
    public class ClinicQueueBL : IClinicQueueBL
    {

        private readonly IManagerDAL _managerDal;


        public ClinicQueueBL(IManagerDAL managerDAL)
        {

            _managerDal = managerDAL;
        }



        //קביעת תור

        //public async Task<bool> MakeAnAppointment(string doctorFirstname, string doctorLastname, DateTime date, string idClient)
        //{
        //    var doctorID = await _managerDal._doctorDAL.SearchADoctor(doctorFirstname, doctorLastname);
        //    if (doctorID == null)
        //        throw new DoctorNotExsistException(doctorFirstname, doctorLastname);

        //    var clientID = _managerDal._clientDAL.GetClientById(idClient);
        //    if (clientID == null)
        //        throw new ClientNotExsistException(idClient);



        //    if (await AvailableQueueManager.Instance.IsHolidayAsync(date))
        //        return false;
        //    return _managerDal._availableQueueDAL.MakeAnAppointment(doctorID, clientID.ClientId, date);

        //}
        public async Task MakeAnAppointment(string idDoctor, string idClient, DateTime date)
        {
            if (await AvailableQueueManager.Instance.IsHolidayAsync(date))
                throw new DayOfQueueIsNotPermission(date);
             Client? client = await _managerDal._clientDAL.GetClientById(idClient);
            if(client==null)
                throw new ClientNotExsistException(idClient);
            int doctorId = await _managerDal._doctorDAL.GetDoctorIdByIdNumber(idDoctor);
            if (doctorId == 0)
                throw new DoctorNotExsistException(idDoctor);
            bool success = await _managerDal._availableQueueDAL.MakeAnAppointment(doctorId, client, date);
            if (!success)
                throw new AvailableQueueNotFoundException(idDoctor, date);

        }
        public async Task DeleteAnApointment(string idDoctor, string idClient, DateTime date)
        {
            
            bool queueDeleted= await _managerDal._clinicQueueDAL.DeleteAnApointment(idDoctor, idClient, date);
            if (!queueDeleted)
                throw new QueueDoesNotExsistException(idDoctor, idClient, date);
        }

        // התור הקרוב לא משנה לאיזה רופא
        //    public async Task<Appointment> GetNearestAppointmentAsync(DateTime targetDate, string idClient)
        //    {

        //        var allAppointments = await _managerDal._availableQueueDAL.GetAllAppointmentsAsync();

        //        var nearest = allAppointments
        //            .Where(a => a.Date >= targetDate)
        //            .OrderBy(a => a.Date)
        //            .FirstOrDefault();
        //        if (nearest == null) return false;
        //        var isBooked = await MakeAnAppointment(nearest.DoctorId.FirstName, nearest.DoctorId.LastName, idClient, nearest.AppointmentDate);
        //        if (!isBooked)
        //            return null;
        //        return isBooked;
        //    }
        public async Task Appointment()
        {
            DateTime start = DateTime.Today;
            DateTime end = start.AddDays(180);

            // Check for null managerDal and doctorDAL
            if (_managerDal == null || _managerDal._doctorDAL == null)
                throw new Exception("_managerDal or _doctorDAL is null");

            var doctors = await _managerDal._doctorDAL.GetDoctorsWithDays();
            if (doctors == null)
                throw new Exception("doctors is null");

            foreach (var doctor in doctors)
            {
                if (doctor.DayDoctors == null) continue; // Skip if no working days

                foreach (var dayDoctor in doctor.DayDoctors)
                {
                    if (dayDoctor.Day == null) continue; // Skip if day is null

                    var day = dayDoctor.Day;

                    for (DateTime date = start; date <= end; date = date.AddDays(1))
                    {
                        if ((int)date.DayOfWeek == day.DayNum)
                        {
                            for (int h = day.StartHour; h < day.EndHour; h++)
                            {
                                var appointmentDate = new DateTime(date.Year, date.Month, date.Day, h, 0, 0);

                                // Check _availableQueueDAL for null
                                if (_managerDal._availableQueueDAL == null)
                                    throw new Exception("_availableQueueDAL is null");

                                // Use the correct type for exists
                                var exists = await _managerDal._availableQueueDAL
                                    .GetDoctorAvailableQueueForASpecificHour(doctor.DoctorId, appointmentDate);

                                if (exists == null)
                                {
                                    var appointment = new AvailableQueue(appointmentDate, doctor);
                                    await _managerDal._availableQueueDAL.AddAvailableQueue(appointment);
                                }
                            }
                        }
                    }
                }
            }
          
        }



        //    //עדכון תור
        //    public async Task<bool> UpdateAppointment(int appointmentId, DateTime newDate)
        //    {



        //    }
        //    public async Task<List<Appointment>> GetAppointmentsForDoctor(int doctorId, DateTime? date = null);
        //    public async Task<List<Appointment>> GetAppointmentsForClient(int clientId);
        //    public async Task<List<DateTime>> GetAvailableSlots(int doctorId, DateTime date);
        //    public async Task<bool> CheckIfSlotAvailable(int doctorId, DateTime dateTime);
        //    public async Task<bool> CancelAppointment(int appointmentId);
        //    public async Task<List<Appointment>> ListAllAppointments();

    }
}
