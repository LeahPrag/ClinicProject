using DAL.Models;
using DAL.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    //public interface IClientBL
    //{
    //    List<Client> GetAllClients();
    //    Client GetClientById(string id);
    //    void AddClient(Client client);
    //    void RemoveClient(string id);
    //    void UpdateClientEmail(String id, string email);
    //    void UpdateClientAdress(String id, string adress);


    //}
    public interface IClientBL
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClientById(string id);
        Task AddClient(Client client);
        Task RemoveClient(string id);
        Task UpdateClient(Client updatedClient, Client existingClient);
        //void UpdateClientEmail(String id, string email);
        //void UpdateClientAdress(String id, string adress);

    }
}
