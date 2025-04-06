using ClientsDataAccessLib.DTOs.Contacts;
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
    /// Contact Service for contact operations
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        //Create new contact
        public async Task AddContactAsync(AddContactDTO contact)
        {
            try
            {
                await _contactRepository.AddAsync(contact);
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to add new contact", "Contact Operation Failed");
            }
        }
        //Delete a contact
        public async Task DeleteContactAsync(Guid contactId)
        {
            try
            {
                await _contactRepository.DeleteAsync(contactId);
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to delete contact", "Contact Operation Failed");
            }
        }

        public IEnumerable<GetContactDTO> GetAllContacts(ListParameters listParameters)
        {
            try
            {
                var contacts = _contactRepository.GetAll(listParameters);
                return contacts;
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to get all contacts", "Contact Operation Failed");
            }
        }

        public IEnumerable<GetContactDTO> GetAllContactsByClientId(Guid clientId)
        {
            try
            {
                var contacts = _contactRepository.GetAllContactsByClientId(clientId);
                return contacts;
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to get all contacts by client id", "Contact Operation Failed");
            }
        }

        //Link a client to a contact
        public async Task LinkClientToContactAsync(Guid contactId, Guid clientId)
        {
            try
            {
                await _contactRepository.LinkToClientAsync(contactId, clientId);
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to link contact to client", "Contact Operation Failed");
            }
        }

        //Update a contact
        public async Task UpdateContactAsync(UpdateContactDTO contact)
        {
            try
            {
                await _contactRepository.UpdateAsync(contact);
            }
            catch (Exception)
            {
                throw new DatabaseOperationException("Unable to update contact", "Contact Operation Failed");
            }
        }
    }
}
