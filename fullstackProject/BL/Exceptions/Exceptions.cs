namespace BL.Exceptions
{
    public class ClientNotExistException : Exception
    {
        public int StatusCode { get; } = 600;
        public ClientNotExistException(string ClientId) : base($"The Client with Id {ClientId} not Exist")
        {
            StatusCode = 600;
        }
    }
    public class ClientAlradyExistException : Exception
    {
        public int StatusCode { get; }
        public ClientAlradyExistException(string ClientId) : base($"The  Client with Id {ClientId} aleady Exist")
        {
            StatusCode = 604;
        }
    }
    public class specializationNotExistException : Exception
    {
        public int StatusCode { get; }
        public specializationNotExistException(string specialization) : base($"The  specialization {specialization} not Exist")
        {
            StatusCode = 605;
        }
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
    public class DoctorAlradyExistException : Exception
    {
        public int StatusCode { get; }
        public DoctorAlradyExistException(string firstName, string lastName) : base($"The doctor: {firstName} {lastName} aleady Exist")
        {
            StatusCode = 624;
        }
    }
    public class QueueDoesNotExistException : Exception
    {
        public int StatusCode { get; }
        public QueueDoesNotExistException(string idDoctor, string idClient, DateTime date) : base($"The queue: doctor: {idDoctor} client:{idClient} date: {date}does not Exist")
        {
            StatusCode = 650;
        }
    }
    public class DayOfQueueIsNotPermission : Exception
    {
        public int StatusCode { get; }
        public DayOfQueueIsNotPermission( DateTime date) : base($"The date: {date} does not permission for a queue")
        {
            StatusCode = 650;
        }
    }
    
    public class AvailableQueueNotFoundException : Exception
    {
        public int StatusCode { get; }
        public AvailableQueueNotFoundException(string idDoctor, DateTime date) : base($"The queue in date: {date:yyyy-MM-dd HH:mm} for doctor {idDoctor} is not available")
        {
            StatusCode = 650;
        }
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

