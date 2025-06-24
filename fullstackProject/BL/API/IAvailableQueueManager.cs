namespace BL.API
{
    public interface IAvailableQueueManager
    {
        public Task<bool> IsHolidayAsync(DateTime date);
    }
}
