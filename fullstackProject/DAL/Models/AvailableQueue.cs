using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class AvailableQueue
{
    public int QueueId { get; set; }

    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public AvailableQueue(int queueId, DateTime appointmentDate, Doctor doctor)
    {
        QueueId = queueId;

        AppointmentDate = appointmentDate;
        Doctor = doctor;
        DoctorId = doctor.DoctorId;
    }
    public AvailableQueue()
    {
    }
    public AvailableQueue(int queueId, int doctorId, DateTime appointmentDate, Doctor doctor)
    {
        QueueId = queueId;
        DoctorId = doctorId;
        AppointmentDate = appointmentDate;
        Doctor = doctor;
    }

}
