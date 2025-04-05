using ClientsDataAccessLib.Data;
using ClientsDataAccessLib.DTOs.Clients;
using ClientsDataAccessLib.Exceptions;
using ClientsDataAccessLib.Models;
using ClientsDataAccessLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Repositories
{
    /// <summary>
    /// Database operations for clients.
    /// </summary>
    public class ClientRepository : BaseRepository, IClientRepository
    {

        public ClientRepository(ApplicationDBContext dbContext):base(dbContext)
        {
            
        }

        //Creates a new client
        public async Task AddAsync(AddClientDTO client)
        {
            var _client = new Client
            {
                Name = client.Name,
                Code = client.Code,
            };
             _dbContext?.Clients.AddAsync(_client);
            await SaveChangesAsync();
        }

        //Deletes a client
        public async Task DeleteAsync(Guid clientId)
        {
            var client = _dbContext?.Clients.FirstOrDefault(x => x.ClientId == clientId);
            if (client is not null)
            {
                //Unlinks all contacts from the client first
                var clientContacts = _dbContext?.ClientContacts.Where(x => x.ClientId == clientId).ToList();
                if(clientContacts is not null)
                    _dbContext?.ClientContacts.RemoveRange(clientContacts);
 
                _dbContext?.Clients.Remove(client);
                
                await SaveChangesAsync();
            }
        }

        //Empty from ClientContacts
        private async Task DeleteClientContactsAsync(Guid clientId)
        {
            var clientContacts = _dbContext?.ClientContacts.Where(c => c.ClientId == clientId).ToList();
            if (clientContacts is not null)
            {
                _dbContext?.ClientContacts.RemoveRange(clientContacts);
                await SaveChangesAsync();
            }
        }


        //Gets all clients
        public IEnumerable<GetClientDTO> GetAll(ListParameters listParams)
        {

            var clients = _dbContext?.Clients
                    .OrderBy(x => x.Name)
                    .GroupJoin(
                        _dbContext.ClientContacts, // Join Clients with ClientContacts
                        clnts => clnts.ClientId,   // Client key
                        clcts => clcts.ClientId,   // ClientContact key
                        (clnts, clcts) => new GetClientDTO
                        {
                            ClientId = clnts.ClientId,
                            Name = clnts.Name,
                            Code = clnts.Code,
                            TotalContacts = clcts.Count() // Count contacts per client
                        })
                        .Skip((listParams.PageNumber - 1) * listParams.PageSize)
                        .Take(listParams.PageSize)
                        .ToList();
            if (clients is null)
                throw new DatabaseOperationException("Unable to get clients please try later", "Operation Failed");
            
            return clients; 

        }

        //Links a contact to a client
        public async Task<bool> LinkToContactAsync(Guid clientId, Guid contactId)
        {
            var isLinked = false; 
            var clientContact = _dbContext?.ClientContacts.Where(x => x.ContactId == contactId && x.ClientId == clientId).FirstOrDefault();
            if(clientContact is null)
            {
                clientContact = new ClientContact
                {
                    ClientId = clientId,
                    ContactId = contactId
                };
                _dbContext?.ClientContacts.Add(clientContact);
                isLinked = true;
            }
            else
            {
                _dbContext?.ClientContacts.Remove(clientContact);
                isLinked = false;
            }
            await SaveChangesAsync();
            return isLinked;
        }

        //Updates a client
        public async Task UpdateAsync(UpdateClientDTO client)
        {
            var _client = _dbContext?.Clients.FirstOrDefault(x => x.ClientId == client.ClientId);
            if (_client is not null)
            {
                _client.Name = client.Name;
                _client.Code = client.Code;
                _dbContext?.Clients.Update(_client);
                await SaveChangesAsync();
            }
            else
            {
                throw new DatabaseOperationException("Unable to update client please try later", "Operation Failed");
            }
        }
    }
}
