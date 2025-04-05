using ClientsDataAccessLib.DTOs.Clients;
using ClientsDataAccessLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Repositories
{
    public interface IClientRepository
    {
        Task AddAsync(AddClientDTO client);

        IEnumerable<GetClientDTO> GetAll(ListParameters listParams);

        Task UpdateAsync(UpdateClientDTO client);

        Task DeleteAsync(Guid clientId);

        Task<bool> LinkToContactAsync(Guid clientId, Guid contactId);
    }
}
