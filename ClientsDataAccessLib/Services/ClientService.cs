using ClientsDataAccessLib.DTOs.Clients;
using ClientsDataAccessLib.Exceptions;
using ClientsDataAccessLib.Repositories;
using ClientsDataAccessLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Services
{
    /// <summary>
    /// Client Service for client operations
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        //Create new client
        public async Task AddClientAsync(AddClientDTO client)
        {
            try
            {
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
    }
}
