using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class UpdateDoctorDto
    {

            public string IdNumber { get; set; } = null!;
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Specialization { get; set; }
        
    }
}
