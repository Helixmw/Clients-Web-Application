using ClientsDataAccessLib.DTOs.Contacts;
using ClientsDataAccessLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Services
{
    public interface IContactService
    {
        Task AddContactAsync(AddContactDTO contact);

        IEnumerable<GetContactDTO> GetAllContacts(ListParameters listParameters);

        IEnumerable<GetContactDTO> GetAllContactsByClientId(Guid clientId);

        Task UpdateContactAsync(UpdateContactDTO contact);

        Task DeleteContactAsync(Guid contactId);

        Task<bool> LinkClientToContactAsync(Guid contactId, Guid clientId);
    }
}
