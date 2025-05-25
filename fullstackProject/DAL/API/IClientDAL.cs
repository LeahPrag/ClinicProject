using DAL.Models;

namespace DAL.service
{
    public interface IClientDAL
    {
        void AddClient(Client client);
        bool ClientExistById(string id);
        List<Client> GetAllClients();
        Client GetClientById(string id);
        void RemoveClient(Client client);
        void UpdateClient(Client updatedClient, Client existingClient);



    }
}