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
        public async Task<Boolean> MakeAnAppointment(string doctorFirstname, string doctorLastname, DateTime date, string idClient)
        {
            var doctorID = await _managerDal._doctorDAL.SearchADoctor(doctorFirstname, doctorLastname);
            if (doctorID == null)
                throw new DoctorNotExsistException(doctorFirstname, doctorLastname);

            var clientID = _managerDal._clientDAL.GetClientById(idClient);
            if (clientID == null)
                throw new ClientNotExsistException(idClient);



            if (await AvailableQueueManager.Instance.IsHolidayAsync(date))
                return false;
            return _managerDal._availableQueueDAL.MakeAnAppointment(doctorID, clientID.ClientId, date);

        }
        // מחיקה

        public Boolean DeleteAnApointment(string doctorFirstname, string doctorLastname, string idClient, DateOnly date)
        {
            int doctorID = _managerDal._doctorDAL.SearchADoctor(doctorFirstname, doctorLastname).Result;

            int clientID = _managerDal._clientDAL.GetClientById(idClient).ClientId;

            return _managerDal._clinicQueueDAL.DeleteAnApointment(doctorID, clientID).Result;// clientID
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
