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

        //List of all patients
        public List<Client> GetAllClients()
        {
            return _dbManager.Clients.ToList();
        }

        //Search for a patient by ID number - returns the patient
        public Client GetClientById(string id)
        {
            return _dbManager.Clients.FirstOrDefault(x => x.IdNumber.Equals(id));
        }

        //Search for a patient by ID-Boolean
        public bool ClientExistById(string id)
        {

            return _dbManager.Clients.Any(x => x.IdNumber.Equals(id));
        }

        public void AddClient(Client client)
        {
            _dbManager.Clients.Add(client);
        }

        public void RemoveClient(Client client)
        {
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
