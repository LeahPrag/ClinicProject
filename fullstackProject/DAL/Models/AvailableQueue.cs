using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class AvailableQueue
{
    public int QueueId { get; set; }

    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}
