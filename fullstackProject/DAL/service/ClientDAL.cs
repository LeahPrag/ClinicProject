using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    public class ClientDAL : IClientDAL
    {
        private readonly DB_Manager _dbManager;
        public ClientDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        //רשימה של כל הפציינטים
        public List<Client> GetAllClients()
        {
            return _dbManager.Clients.ToList();
        }

        //חיפוש פציינט ע"פ ת"ז-מחזיר את הפציינט
        public Client GetClientById(string id)
        {
            List<Client> clients = _dbManager.Clients.ToList();
            return clients.FirstOrDefault(x => x.ClientId.Equals(id));
        }

        //חיפוש פציינט ע"פ ת"ז-בוליאני
        public bool ClientExistById(string id)
        {
            List<Client> clients = _dbManager.Clients.ToList();
            Client c = clients.FirstOrDefault(x => x.ClientId.Equals(id));
            if (c == null) return false;
            return true;
        }

        //מוסיף פציינט
        public void AddClient(Client client)
        {
            _dbManager.Clients.Add(client);
        }

        //מוחק פציינט
        public void RemoveClient(string id)
        {
            Client client = GetClientById(id);
            _dbManager.Clients.Remove(client);
        }

        //מעדכן פרטי פציינט קיים אם קיים
        public void UpdateClient(Client client)
        {
            List<Client> clients = _dbManager.Clients.ToList();
            if (!ClientExistById(client.IDNumber))
            {
                // error- the client not defined
            }
            _dbManager.Clients.Update(client);
        }

    }
}
