using DAL.Models;
namespace DAL.service
{
    public interface IClientDAL
    {
        Task<List<Client>> GetAllClients();
        Task<Client?> GetClientById(string id);
        Task<bool> ClientExistById(string id);
        Task AddClient(Client client);
        void RemoveClient(Client client);
        void UpdateClient(Client updatedClient, Client existingClient);
    }
}
