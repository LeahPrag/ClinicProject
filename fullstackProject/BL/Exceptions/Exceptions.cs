using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
    public class DoctorAlradyExsistException : Exception
    {

        public int StatusCode { get; }
        public DoctorAlradyExsistException(string firstName, string lastName) : base($"The doctor: {firstName} {lastName} aleady exsist")
        {
            StatusCode = 624;
        }
    }




}

