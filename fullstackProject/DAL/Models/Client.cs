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

    public virtual ICollection<AvailableQueue> AvailableQueues { get; set; } = new List<AvailableQueue>();

    public virtual ICollection<ClinicQueue> ClinicQueues { get; set; } = new List<ClinicQueue>();
}
