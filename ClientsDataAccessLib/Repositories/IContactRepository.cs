using ClientsDataAccessLib.DTOs.Contacts;
using ClientsDataAccessLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Repositories
{
    public interface IContactRepository
    {
        Task AddAsync(AddContactDTO contact);

        IEnumerable<GetContactDTO> GetAll(ListParameters listParams);

        Task UpdateAsync(UpdateContactDTO contact);

        Task DeleteAsync(Guid contactId);

        Task LinkToClientAsync(Guid contactId, Guid clientId);

        IEnumerable<GetContactDTO> GetAllContactsByClientId(Guid clientId);
    }
}
