using DAL.Models;
using BL.API;
using DAL.API;
using BL.Exceptions;
using System.Text.RegularExpressions;
using AutoMapper;
using BL.Models;
using DAL.service;

namespace BL.service
{
    public class ClientBL : IClientBL
    {
        private readonly IClientDAL _clientDal;
        private readonly IMapper _mapper;

        public ClientBL(IManagerDAL managerDAL, IMapper mapper)
        {
            _clientDal = managerDAL._clientDAL;
            _mapper = mapper;
        }
        public async Task<List<M_Client>> GetAllClients()
        {
            var clients = await _clientDal.GetAllClients();
            return _mapper.Map<List<M_Client>>(clients);
        }
        public async Task<M_Client> GetClientById(string id)
        {
            var client = await _clientDal.GetClientById(id);
            return client == null ? throw new ClientNotExistException(id) : _mapper.Map<M_Client>(client);
        }
        public async Task AddClient(M_Client mClient)
        {
            if (await _clientDal.ClientExistById(mClient.IdNumber))
                throw new ClientAlreadyExistException(mClient.IdNumber);

            if (!IsValidInput(mClient.FirstName) || !IsValidInput(mClient.LastName) || !IsValidEmail(mClient.Email) || !IsValidPhone(mClient.Phone))
                throw new IncompatibleOrIincompleteValuesException();

            var client = _mapper.Map<Client>(mClient);
            await _clientDal.AddClient(client);
        }

        public async Task RemoveClient(string id)
        {
            var client = await _clientDal.GetClientById(id) ?? throw new ClientNotExistException(id);
            await Task.Run(() => _clientDal.RemoveClient(client));
        }

        public async Task UpdateClient(M_Client updatedMClient, string existingClientId)
        {
            var existingClient = await _clientDal.GetClientById(existingClientId) ?? throw new ClientNotExistException(existingClientId);
            if (!IsValidEmail(updatedMClient.Email) ||
                !IsValidInput(updatedMClient.LastName) ||
                !IsValidPhone(updatedMClient.Phone) ||
                !IsValidInput(updatedMClient.Address))
                throw new IncompatibleOrIincompleteValuesException();

            var updatedClient = _mapper.Map<Client>(updatedMClient);
            await _clientDal.UpdateClient(updatedClient, existingClient);
        }

        public static bool IsValidDateOfBirth(DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var earliest = today.AddYears(-120);
            return date <= today && date >= earliest;
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
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

        public static bool IsValidPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            var pattern = @"^\d{10,15}$";
            return Regex.IsMatch(phone, pattern);
        }

        public static bool IsValidInput(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}
