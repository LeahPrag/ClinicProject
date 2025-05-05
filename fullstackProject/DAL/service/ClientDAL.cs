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
        public Client GetClientById(int id)
        {
            List<Client> clients = _dbManager.Clients.ToList();
            return clients.FirstOrDefault(x => x.ClientId == id);
        }

        //חיפוש פציינט ע"פ ת"ז-בוליאני
        public bool ClientExistById(int id)
        {
            List<Client> clients = _dbManager.Clients.ToList();
            Client c = clients.FirstOrDefault(x => x.ClientId == id);
            if (c == null) return false;
            return true;
        }

        //מוסיף פציינט
        public void AddClient(Client client)
        {
            _dbManager.Clients.Add(client);
        }

        //מוחק פציינט
        public void RemoveClient(Client client)
        {
            _dbManager.Clients.Remove(client);
        }

        //מעדכן פרטי פציינט קיים אם קיים
        public void UpdatePatient(Client client)
        {
            List<Client> clients = _dbManager.Clients.ToList();
            if (!ClientExistById(client.ClientId))
            {
                // error- the client not defined
            }
            _dbManager.Clients.Update(client);
        }
        public int SearchClient(string client_firtsname, string client_lastname)
        {

            List<Doctor> clients = _dbManager.Doctors.ToList();
            Client c = clients.FirstOrDefault(x => x.FirstName.Equals(client_firtsname) && x.FirstName.Equals(client_lastname));
            if (c != null)
            {
                return c.ClientId;
            }
            return -1;

        }
    }
}
