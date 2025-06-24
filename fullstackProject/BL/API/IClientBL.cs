using DAL.Models;
namespace BL.API
{
    public interface IClientBL
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClientById(string id);
        Task AddClient(Client client);
        Task RemoveClient(string id);
        Task UpdateClient(Client updatedClient, Client existingClient);
    }
}
