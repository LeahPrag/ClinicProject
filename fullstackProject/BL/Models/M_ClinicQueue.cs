namespace BL.Models
{
	public class M_ClinicQueue
	{
		public int QueueId { get; set; }
		public DateTime AppointmentDate { get; set; }
		public int ClientId { get; set; }
		public string ClientFirstName { get; set; } = "";
		public string ClientLastName { get; set; } = "";
		public int DoctorId { get; set; }
		public string DoctorFirstName { get; set; } = "";
		public string DoctorLastName { get; set; } = "";
	}
}