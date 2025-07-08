using DAL.Models;
using BL.Models;
namespace BL.API
{
    public interface IClientBL
    {
        Task<List<M_Client>> GetAllClients();
        Task<M_Client> GetClientById(string id);
        Task AddClient(M_Client client);
        Task RemoveClient(string id);
        Task UpdateClient(M_Client updatedClient, string existingClientId);
    }
}
