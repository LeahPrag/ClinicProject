using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Day
{
    public int Id { get; set; }

    public int DayNum { get; set; }

    public int StartHour { get; set; }

    public int EndHour { get; set; }

    public virtual ICollection<DayDoctor> DayDoctors { get; set; } = new List<DayDoctor>();
    public Day(int id, int dayNum, int startHour, int endHour, ICollection<DayDoctor>? dayDoctors = null)
    {
        Id = id;
        DayNum = dayNum;
        StartHour = startHour;
        EndHour = endHour;
        DayDoctors = dayDoctors ?? new List<DayDoctor>();
    }
    public Day() { }
}
