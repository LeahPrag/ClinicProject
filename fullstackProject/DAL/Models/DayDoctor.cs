namespace DAL.Models;
public partial class DayDoctor
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int DayId { get; set; }
    public virtual Day Day { get; set; } = null!;
    public virtual Doctor Doctor { get; set; } = null!;
    public DayDoctor(int id, int doctorId, int dayId, Day day, Doctor doctor)
    {
        Id = id;
        DoctorId = doctorId;
        DayId = dayId;
        Day = day;
        Doctor = doctor;
    }
    public DayDoctor()
    {
    }
}
