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
            return _dbManager.Clients.FirstOrDefault(x => x.IDNumber.Equals(id));
            //List<Client> clients = _dbManager.Clients.ToList();
            //return clients.FirstOrDefault(x => x.ClientId.Equals(id));
        }

        //חיפוש פציינט ע"פ ת"ז-בוליאני
        public bool ClientExistById(string id)
        {
            //List<Client> clients = _dbManager.Clients.ToList();
            //Client c = clients.FirstOrDefault(x => x.ClientId.Equals(id));
            //if (c == null) return false;
            //return true;
            return _dbManager.Clients.Any(x => x.IDNumber.Equals(id));
        }

        //מוסיף פציינט
        public void AddClient(Client client)
        {
            _dbManager.Clients.Add(client);
        }

        //מוחק פציינט
        public void RemoveClient(Client client)
        {
            //Client client = GetClientById(id);
            _dbManager.Clients.Remove(client);
        }



        public void UpdateClient(Client updatedClient, Client existingClient)
        {
         
            existingClient.LastName = updatedClient.LastName;
            existingClient.Phone = updatedClient.Phone;
            existingClient.Email = updatedClient.Email;
            existingClient.Address = updatedClient.Address;
            _dbManager.Clients.Update(existingClient);
        }

        
    }
}
