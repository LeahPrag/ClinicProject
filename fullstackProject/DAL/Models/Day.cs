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
}
