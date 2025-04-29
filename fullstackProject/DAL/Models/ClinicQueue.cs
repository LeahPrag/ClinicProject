using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ClinicQueue
{
    public int QueueId { get; set; }

    public int ClientId { get; set; }

    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;
}
