namespace BL.API
{
    public interface IManagerBL
    {
        public IClientBL _clientBL { get; set; }
        public IDoctorBL _doctorBL { get; set; }
        public IClinicQueueBL _clinicQueueBL { get; set; }
    }
}
