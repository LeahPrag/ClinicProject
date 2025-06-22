using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Client>> GetAllClients()
        {
            return await _dbManager.Clients.ToListAsync();
        }

        //Search for a patient by ID number - returns the patient
        //        //חיפוש פציינט ע"פ ת"ז-מחזיר את הפציינט
        public async Task<Client?> GetClientById(string id)
        {
            return await _dbManager.Clients.FirstOrDefaultAsync(x => x.IdNumber.Equals(id));
        }

        //Search for a patient by ID-Boolean
        //        //חיפוש פציינט ע"פ ת"ז-בוליאני
        public async Task<bool> ClientExistById(string id)
        {

            return await _dbManager.Clients.AnyAsync(x => x.IdNumber.Equals(id));
        }

        //מוסיף פציינט
        public async Task AddClient(Client client)
        {
            await _dbManager.Clients.AddAsync(client);
        }

        public void RemoveClient(Client client)
        {
            _dbManager.Clients.Remove(client);
            _dbManager.SaveChanges();// leahle added it
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
