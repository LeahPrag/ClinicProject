using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.API
{
    public interface IAvailableQueueDAL
    {
        Boolean MakeAnAppointment(int doctorId, int clientId, DateTime dateTime);
        Task<List<AvailableQueue>> GetDoctorAvailableQueueForASpesificDay(int doctorId, DateOnly day);
        Boolean MakeAnAppointment(DateTime dateTime);
        Boolean MakeAnAppointment(int doctorId, DateTime dateTime, Client client);
        Task<List<AvailableQueue>> GetAvailableQueueForASpesificDay(DateOnly day);
    }
}
