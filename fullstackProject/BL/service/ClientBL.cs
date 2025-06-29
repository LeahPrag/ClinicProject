using DAL.Models;
using BL.API;
using DAL.API;
using BL.Exceptions;
using System.Text.RegularExpressions;
using DAL.service;

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
            try
            {
                return await _clientDal.GetAllClients();
            }
            catch (Exception ex)
            {
                throw new Exception("BL Error - GetAllClients: " + ex.Message, ex);
            }
        }

        public async Task<Client> GetClientById(string id)
        {
            try
            {
                var client = await _clientDal.GetClientById(id);
                return client ?? throw new ClientNotExistException(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BL Error - GetClientById: " + ex.Message, ex);
            }
        }

        public async Task AddClient(Client client)
        {
            try
            {
                if (await _clientDal.ClientExistById(client.IdNumber))
                    throw new ClientAlreadyExistException(client.IdNumber);

                if (!IsValidInput(client.FirstName) || !IsValidInput(client.LastName))
                    throw new IncompatibleOrIincompleteValuesException();

                if (client.IdNumber.Length != 9 || !IsValidDateOfBirth(client.BirthDate))
                    throw new IncompatibleOrIincompleteValuesException();

                await _clientDal.AddClient(client);
            }
            catch (Exception ex)
            {
                throw new Exception("BL Error - AddClient: " + ex.Message, ex);
            }
        }

        public async Task RemoveClient(string id)
        {
            try
            {
                var client = await GetClientById(id);
                await Task.Run(() => _clientDal.RemoveClient(client));
            }
            catch (Exception ex)
            {
                throw new Exception("BL Error - RemoveClient: " + ex.Message, ex);
            }
        }

        public async Task UpdateClient(Client updatedClient, Client existingClient)
        {
            try
            {
                await GetClientById(existingClient.IdNumber);

                if (!IsValidEmail(updatedClient.Email) ||
                    !IsValidInput(updatedClient.LastName) ||
                    !IsValidPhone(updatedClient.Phone) ||
                    !IsValidInput(updatedClient.Address))
                    throw new IncompatibleOrIincompleteValuesException();

                await _clientDal.UpdateClient(updatedClient, existingClient);
            }
            catch (Exception ex)
            {
                throw new Exception("BL Error - UpdateClient: " + ex.Message, ex);
            }
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
