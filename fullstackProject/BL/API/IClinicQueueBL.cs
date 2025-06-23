using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IClinicQueueBL
    {
        Task DeleteAnApointment(string udDoctor, string idClient, DateTime date);
        Task Appointment();
        Task MakeAnAppointment(string idDoctor, string idClient, DateTime date);
    }
}
