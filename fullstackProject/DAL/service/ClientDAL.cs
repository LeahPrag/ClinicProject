using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    internal class ClientDAL
    {
        private readonly DB_Manager _dbManager;
        public ClientDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        public List<Client> GetAllPatients()
        {
            return _dbManager.Client.ToList();
        }
    }
}
