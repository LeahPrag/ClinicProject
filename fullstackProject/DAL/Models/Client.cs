using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateOnly BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public string IdNumber { get; set; } = null!;

    public virtual ICollection<ClinicQueue> ClinicQueues { get; set; } = new List<ClinicQueue>();
    public Client()
    {

    }
    public Client(int clientId, string firstName, string lastName, string? phone, string? email, DateOnly birthDate, string address, string idNumber) {
        ClientId = clientId;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        BirthDate = birthDate;
        Address = address;
        IdNumber = idNumber; 
        ClinicQueues = new List<ClinicQueue>();
    }
}
