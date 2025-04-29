using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Specialization { get; set; }=null!;

    public virtual ICollection<AvailableQueue> AvailableQueues { get; set; } = new List<AvailableQueue>();

    public virtual ICollection<ClinicQueue> ClinicQueues { get; set; } = new List<ClinicQueue>();

    public virtual ICollection<DayDoctor> DayDoctors { get; set; } = new List<DayDoctor>();
}
