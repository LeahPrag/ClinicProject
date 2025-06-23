using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL.Exceptions
{
    public class ClientNotExsistException : Exception
    {

        public int StatusCode { get; }

        public ClientNotExsistException(string ClientId) : base($"The Client with Id {ClientId} not exsist")
        {
            StatusCode = 600;
        }

    }
    public class ClientAlradyExsistException : Exception
    {
        public int StatusCode { get; }
        public ClientAlradyExsistException(string ClientId) : base($"The  Client with Id {ClientId} aleady exsist")
        {
            StatusCode = 604;
        }

    }
    public class specializationNotExistException : Exception
        {
            public int StatusCode { get; }

            public specializationNotExistException(string specialization) : base($"The  specialization {specialization} not exsist")
            {
                StatusCode = 605;
            }

        }

    public class DoctorNotExsistException : Exception
    {

        public int StatusCode { get; }
        public DoctorNotExsistException(string firstName, string lastName) : base($"The doctor: {firstName} {lastName} not exsist")
        {
            StatusCode = 620;
        }
        public DoctorNotExsistException(string id) : base($"The doctor: {id} not exsist")
        {
            StatusCode = 621;
        }
    }
    public class DoctorAlradyExsistException : Exception
    {

        public int StatusCode { get; }
        public DoctorAlradyExsistException(string firstName, string lastName) : base($"The doctor: {firstName} {lastName} aleady exsist")
        {
            StatusCode = 624;
        }
    }
    public class QueueDoesNotExsistException : Exception
    {

        public int StatusCode { get; }
        public QueueDoesNotExsistException(string idDoctor, string idClient, DateTime date) : base($"The queue: doctor: {idDoctor} client:{idClient} date: {date}does not exsist")
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

