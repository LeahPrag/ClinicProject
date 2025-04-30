using DAL.Models;

namespace DAL.service
{
    public interface IClientDAL
    {
        void AddClient(Client client);
        bool ClientExistById(int id);
        List<Client> GetAllClients();
        Client GetClientById(int id);
        void RemoveClient(Client client);
        void UpdatePatient(Client client);
    }
}