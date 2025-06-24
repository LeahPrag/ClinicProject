namespace BL.API
{
    public interface IClinicQueueBL
    {
        Task DeleteAnApointment(string udDoctor, string idClient, DateTime date);
        Task GenerateFutureAvailableQueues();
        Task MakeAnAppointment(string idDoctor, string idClient, DateTime date);
    }
}
