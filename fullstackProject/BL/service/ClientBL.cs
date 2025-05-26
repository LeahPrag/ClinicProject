using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.API;
using DAL.API;
using DAL.service;
using BL.Exceptions;

namespace DAL.service
{
    public class ClientBL : IClientBL
    {
        private readonly IClientDAL _clientDal;

        public ClientBL(IManagerDAL managerDAL)
        {
            _clientDal = managerDAL._clientDAL;
        }

        public List<Client> GetAllClients()
        {
            return _clientDal.GetAllClients();
        }

        public Client GetClientById(string id)
        {
            var client = _clientDal.GetClientById(id);
            if (client == null)
                throw new ClientNotExsistException(id);
            return client;
        }

        public void AddClient(Client client)
        {
            if (_clientDal.ClientExistById(client.IdNumber))
                throw new ClientAlradyExsistException(client.IdNumber);

            if (string.IsNullOrWhiteSpace(client.FirstName) || string.IsNullOrWhiteSpace(client.LastName))
                throw new ArgumentException("First name and last name are required fields.");

            _clientDal.AddClient(client);
        }

        public void RemoveClient(string id)
        {
            var client = GetClientById(id);
            if (client == null)
                throw new ClientNotExsistException(id);
            _clientDal.RemoveClient(client);
        }
        public void UpdateClientEmail(string id, string email)
        {
            var client = GetClientById(id);
            if (client == null)
                throw new ClientNotExsistException(id);
            var updateClient = new Client
            {
                ClientId = client.ClientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = email, 
                BirthDate = client.BirthDate,
                Address = client.Address,
                IdNumber = client.IdNumber,
                ClinicQueues = client.ClinicQueues
            };

            _clientDal.UpdateClient(updateClient, client);
        }
        public void UpdateClientAdress(string id,String adress)
        {
            var client = GetClientById(id);
            if (client == null)
                throw new ClientNotExsistException(id);
            var updateClient = new Client
            {
                ClientId = client.ClientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = client.Email,
                BirthDate = client.BirthDate,
                Address = adress,
                IdNumber = client.IdNumber,
                ClinicQueues = client.ClinicQueues
            };

            _clientDal.UpdateClient(updateClient, client);
        }
    }
}
