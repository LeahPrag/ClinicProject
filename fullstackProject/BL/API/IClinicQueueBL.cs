namespace BL.API
{
    public interface IClinicQueueBL
    {
        Task DeleteAnApointment(string udDoctor, string idClient, DateOnly date);
        Task GenerateFutureAvailableQueues();
        Task MakeAnAppointment(string idDoctor, string idClient, DateOnly date, int hour);
    }
}
