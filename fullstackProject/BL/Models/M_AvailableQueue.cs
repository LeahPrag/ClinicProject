namespace BL.Models
{
	public class M_AvailableQueue
	{
		public int QueueId { get; set; }

		public DateTime AppointmentDate { get; set; }
		public int DoctorId { get; set; }

		public string FirstName { get; set; } = null!;

		public string LastName { get; set; } = null!;

	}
}
