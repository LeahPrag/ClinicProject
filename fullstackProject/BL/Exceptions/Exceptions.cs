namespace BL.Exceptions
{
    public class ClientNotExistException(string ClientId) : Exception($"The Client with Id {ClientId} not Exist")
    {
        public int StatusCode { get; } = 600;
    }
    public class ClientAlreadyExistException(string ClientId) : Exception($"The  Client with Id {ClientId} aleady Exist")
    {
        public int StatusCode { get; } = 604;
    }
    public class SpecializationNotExistException(string specialization) : Exception($"The  specialization {specialization} not Exist")
    {
        public int StatusCode { get; } = 605;
    }
    public class DoctorNotExistException : Exception
    {
        public int StatusCode { get; }
        public DoctorNotExistException(string firstName, string lastName) : base($"The doctor: {firstName} {lastName} not Exist")
        {
            StatusCode = 620;
        }
        public DoctorNotExistException(string id) : base($"The doctor: {id} not Exist")
        {
            StatusCode = 621;
        }
    }
    public class DoctorAlreadyExistException(string firstName, string lastName) : Exception($"The doctor: {firstName} {lastName} aleady Exist")
    {
        public int StatusCode { get; } = 624;
    }
    public class QueueDoesNotExistException(string idDoctor, string idClient, DateTime date) : Exception($"The queue: doctor: {idDoctor} client:{idClient} date: {date}does not Exist")
    {
        public int StatusCode { get; } = 650;
    }
    public class DayOfQueueIsNotPermission(DateTime date) : Exception($"The date: {date} does not permission for a queue")
    {
        public int StatusCode { get; } = 650;
    }
    public class AvailableQueueNotFoundException(string idDoctor, DateTime date) : Exception($"The queue in date: {date:yyyy-MM-dd HH:mm} for doctor {idDoctor} is not available")
    {
        public int StatusCode { get; } = 650;
    }
    public class IncompatibleOrIincompleteValuesException : Exception
    {
        public int StatusCode { get; }
        public IncompatibleOrIincompleteValuesException() : base($"Incompatible or incomplete values")
        {
            StatusCode = 699;
        }
    }
}

