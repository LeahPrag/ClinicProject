using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IManagerBL
    {

        public IClientBL _clientBL { get; set; }
        public IDoctorBL _doctorBL { get; set; }
        public IClinicQueueBL _clinicQueueBL { get; set; }
    }
}
