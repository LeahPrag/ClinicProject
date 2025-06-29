using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.service
{
    public class ClientDAL : IClientDAL
    {
        private readonly DB_Manager _dbManager;
        public ClientDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        public async Task<List<Client>> GetAllClients()
        {
            try
            {
                return await _dbManager.Clients.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error - GetAllClients: " + ex.Message, ex);
            }
        }
        public async Task<Client?> GetClientById(string id)
        {
            try
            {
                return await _dbManager.Clients.FirstOrDefaultAsync(x => x.IdNumber == id);
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error - GetClientById: " + ex.Message, ex);
            }
        }
        public async Task<bool> ClientExistById(string id)
        {
            try
            {
                return await _dbManager.Clients.AnyAsync(x => x.IdNumber == id);
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error - ClientExistById: " + ex.Message, ex);
            }
        }
        public async Task AddClient(Client client)
        {
            try
            {
                await _dbManager.Clients.AddAsync(client);
                await _dbManager.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error - AddClient: " + ex.Message, ex);
            }
        }
        public void RemoveClient(Client client)
        {
            try
            {
                _dbManager.Clients.Remove(client);
                _dbManager.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error - RemoveClient: " + ex.Message, ex);
            }
        }
        public async Task UpdateClient(Client updatedClient, Client existingClient)
        {
            try
            {
                existingClient.LastName = updatedClient.LastName;
                existingClient.Phone = updatedClient.Phone;
                existingClient.Email = updatedClient.Email;
                existingClient.Address = updatedClient.Address;
                _dbManager.Clients.Update(existingClient);
                await _dbManager.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("DAL Error - UpdateClient: " + ex.Message, ex);
            }
        }
    }
}
