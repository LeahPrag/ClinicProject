namespace BL.Models
{
    public class M_Doctor
    {
        public int DoctorId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Specialization { get; set; } = null!;
        public string IdNumber { get; set; } = null!;
    }
}
