using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class M_ClinicQueue
    {
        public int QueueId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public virtual Client Client { get; set; } = null!;

        public virtual Doctor Doctor { get; set; } = null!;
    }
}
