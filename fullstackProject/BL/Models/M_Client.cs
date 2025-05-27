using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class M_Client
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string Address { get; set; } = null!;

        public string IdNumber { get; set; } = null!;

    }
}
