using ClientsDataAccessLib.DTOs.Clients;
using ClientsDataAccessLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Services
{
    public interface IClientService
    {
        Task AddClientAsync(AddClientDTO client);

        IEnumerable<GetClientDTO> GetAllClientsAsync(ListParameters listParameters);

        Task UpdateClientAsync(UpdateClientDTO client);

        Task DeleteClientAsync(Guid clientId);

        Task<bool> LinkContactToClientAsync(Guid clientId, Guid contactId);

        IEnumerable<GetClientDTO> GetAllClientsByContactId(Guid contactId);
    }
}
