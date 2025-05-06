using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IClinicQueueBL
    {
        Boolean DeleteAnApointment(string doctorFirstname, string doctorLastname, string clientFirstname, string clientLastname, DateOnly date);
    }
}
