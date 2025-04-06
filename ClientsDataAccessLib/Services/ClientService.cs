using ClientsDataAccessLib.DTOs.Clients;
using ClientsDataAccessLib.Exceptions;
using ClientsDataAccessLib.Models;
using ClientsDataAccessLib.Repositories;
using ClientsDataAccessLib.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Services
{
    /// <summary>
    /// Client Service for client operations
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        private static int counter = 1;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        //Create new client
        public async Task AddClientAsync(AddClientDTO client)
        {
            try
            {
                var code = GenerateClientCode(client.Name);
                client.Code = code;
                await _clientRepository.AddAsync(client);
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to add new client", "Client Operation Failed");
            }
        }

        //Delete a client
        public async Task DeleteClientAsync(Guid clientId)
        {
            try
            {
                await _clientRepository.DeleteAsync(clientId);
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to delete client", "Client Operation Failed");
            }
        }

        //Gets all clients
        public IEnumerable<GetClientDTO> GetAllClientsAsync(ListParameters listParameters)
        {
            try
            {
                var clients = _clientRepository.GetAll(listParameters);
                return clients;
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to get all clients", "Client Operation Failed");
            }
        }

        //Get a contact to a client
        public async Task<bool> LinkContactToClientAsync(Guid clientId, Guid contactId)
        {
            try
            {
                var isLinked = await _clientRepository.LinkToContactAsync(clientId, contactId);
                return isLinked;
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to link contact to client", "Client Operation Failed");
            }
        }

        //Update a client
        public async Task UpdateClientAsync(UpdateClientDTO client)
        {
            try
            {
                await _clientRepository.UpdateAsync(client);
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to update client", "Client Operation Failed");
            }
        }

        //Generate client code
        private static string GenerateClientCode(string clientName)
        {
            

            // Remove non-alphabetic characters and convert to uppercase
            string cleanedName = Regex.Replace(clientName.ToUpper(), "[^A-Z]", "");

            // Take up to the first 3 letters
            StringBuilder alphaPart = new StringBuilder(cleanedName.Length > 3 ? cleanedName.Substring(0, 3) : cleanedName);

            // If name is less than 3 letters, pad with letters starting from 'A'
            char padChar = 'A';
            while (alphaPart.Length < 3)
            {
                alphaPart.Append(padChar);
                padChar++;
                if (padChar > 'Z') padChar = 'A'; // Wrap around if needed
            }

            // Generate the numeric part with leading zeros
            string numericPart = counter.ToString("D3");

            // Increment counter for next generation
            counter++;

            return $"{alphaPart}{numericPart}";
        }

        public IEnumerable<GetClientDTO> GetAllClientsByContactId(Guid contactId)
        {
            try
            {
                var clients = _clientRepository.GetAllClientsByContactId(contactId);
                return clients;
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to get all clients by contact id", "Contact Operation Failed");
            }
        }
    }
}
