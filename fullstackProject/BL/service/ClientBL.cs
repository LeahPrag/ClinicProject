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
using System.Text.RegularExpressions;

namespace BL.service
{
    public class ClientBL : IClientBL
    {
        private readonly IClientDAL _clientDal;

        public ClientBL(IManagerDAL managerDAL)
        {
            _clientDal = managerDAL._clientDAL;
        }
        public async Task<List<Client>> GetAllClients()
        {
            return await _clientDal.GetAllClients();
        }

        public async Task<Client> GetClientById(string id)
        {
            var client = await _clientDal.GetClientById(id);
            if (client == null)
                throw new ClientNotExsistException(id);
            return client;
        }

        public async Task AddClient(Client client)
        {
            if (await _clientDal.ClientExistById(client.IdNumber))
                throw new ClientAlradyExsistException(client.IdNumber);

            if (IsValidInput(client.FirstName) || IsValidInput(client.LastName))
                throw new IncompatibleOrIincompleteValuesException();
            if (client.IdNumber.Length != 9 || IsValidDateOfBirth(client.BirthDate))
                throw new IncompatibleOrIincompleteValuesException();
            _clientDal.AddClient(client);
        }

        public async Task RemoveClient(string id)
        {
            var client = await GetClientById(id);
            await Task.Run(() => _clientDal.RemoveClient(client));
        }

        public async Task UpdateClient(Client updatedClient, Client existingClient)
        {
            await GetClientById(existingClient.IdNumber);
            /////////////////
            if (!IsValidEmail(updatedClient.Email) || !IsValidInput(updatedClient.LastName) ||
               !IsValidPhone(updatedClient.Phone) || !IsValidInput(updatedClient.Address))
                throw new IncompatibleOrIincompleteValuesException();
            _clientDal.UpdateClient(updatedClient, existingClient);
        }

        public bool IsValidDateOfBirth(DateOnly date)// ez function for Integrity check date
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var earliest = today.AddYears(-120);
            return date <= today && date >= earliest;
        }
        public bool IsValidEmail(string email)// ez function for Integrity check email
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValidPhone(string phone)
        {
            // Example: Validates 10-15 digits (international format, no letters)
            var pattern = @"^\d{10,15}$";
            return Regex.IsMatch(phone, pattern);
        }
        public bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new IncompatibleOrIincompleteValuesException();
            return true;
        }

        //public void UpdateClientEmail(string id, string email)
        //{
        //    var client = GetClientById(id);
        //    if (client == null)
        //        throw new ClientNotExsistException(id);
        //    var updateClient = new Client
        //    {
        //        ClientId = client.ClientId,
        //        FirstName = client.FirstName,
        //        LastName = client.LastName,
        //        Phone = client.Phone,
        //        Email = email,
        //        BirthDate = client.BirthDate,
        //        Address = client.Address,
        //        IdNumber = client.IdNumber,
        //        ClinicQueues = client.ClinicQueues
        //    };
        //    _clientDal.UpdateClient(updateClient, client);
        //}

        //public void UpdateClientAdress(string id,String adress)
        //{
        //    var client = GetClientById(id);
        //    if (client == null)
        //        throw new ClientNotExsistException(id);
        //    var updateClient = new Client
        //    {
        //        ClientId = client.ClientId,
        //        FirstName = client.FirstName,
        //        LastName = client.LastName,
        //        Phone = client.Phone,
        //        Email = client.Email,
        //        BirthDate = client.BirthDate,
        //        Address = adress,
        //        IdNumber = client.IdNumber,
        //        ClinicQueues = client.ClinicQueues
        //    };

        //    _clientDal.UpdateClient(updateClient, client);
        //}
    }

    //    public List<Client> GetAllClients()
    //    {
    //        return _clientDal.GetAllClients();
    //    }

    //    public Client GetClientById(string id)
    //    {
    //        var client = _clientDal.GetClientById(id);
    //        if (client == null)
    //            throw new ClientNotExsistException(id);
    //        return client;
    //    }

    //    public void AddClient(Client client)
    //    {
    //        if (_clientDal.ClientExistById(client.IdNumber))
    //            throw new ClientAlradyExsistException(client.IdNumber);

    //        if (string.IsNullOrWhiteSpace(client.FirstName) || string.IsNullOrWhiteSpace(client.LastName))
    //            throw new ArgumentException("First name and last name are required fields.");

    //        _clientDal.AddClient(client);
    //    }

    //    public void RemoveClient(string id)
    //    {
    //        var client = GetClientById(id);
    //        if (client == null)
    //            throw new ClientNotExsistException(id);
    //        _clientDal.RemoveClient(client);
    //    }
    //    public void UpdateClientEmail(string id, string email)
    //    {
    //        var client = GetClientById(id);
    //        if (client == null)
    //            throw new ClientNotExsistException(id);
    //        var updateClient = new Client
    //        {
    //            ClientId = client.ClientId,
    //            FirstName = client.FirstName,
    //            LastName = client.LastName,
    //            Phone = client.Phone,
    //            Email = email, 
    //            BirthDate = client.BirthDate,
    //            Address = client.Address,
    //            IdNumber = client.IdNumber,
    //            ClinicQueues = client.ClinicQueues
    //        };

    //        _clientDal.UpdateClient(updateClient, client);
    //    }
    //    public void UpdateClientAdress(string id,String adress)
    //    {
    //        var client = GetClientById(id);
    //        if (client == null)
    //            throw new ClientNotExsistException(id);
    //        var updateClient = new Client
    //        {
    //            ClientId = client.ClientId,
    //            FirstName = client.FirstName,
    //            LastName = client.LastName,
    //            Phone = client.Phone,
    //            Email = client.Email,
    //            BirthDate = client.BirthDate,
    //            Address = adress,
    //            IdNumber = client.IdNumber,
    //            ClinicQueues = client.ClinicQueues
    //        };

    //        _clientDal.UpdateClient(updateClient, client);
    //    }
    //}
}
