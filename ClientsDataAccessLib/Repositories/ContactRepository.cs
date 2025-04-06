using ClientsDataAccessLib.Data;
using ClientsDataAccessLib.DTOs.Contacts;
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
    /// Database operations for contacts.
    /// </summary>
    public class ContactRepository : BaseRepository, IContactRepository
    {

        public ContactRepository(ApplicationDBContext dbContext):base(dbContext)
        {
            
        }
        
        //Creates a new contact
        public async Task AddAsync(AddContactDTO contact)
        {
            var _contact = new Contact
            {
                Name = contact.Name,
                Surname = contact.Surname,
                Email = contact.Email,
            };
            _dbContext?.Contacts.AddAsync(_contact);
            await SaveChangesAsync();
        }

        //Deletes a contact
        public async Task DeleteAsync(Guid contactId)
        {
            var contact = _dbContext?.Contacts.FirstOrDefault(c => c.ContactId == contactId);
            if (contact is not null)
            {
                await DeleteContactClientsAsync(contactId); //Delete all contact clients first
                _dbContext?.Contacts.Remove(contact);
                await SaveChangesAsync();
            }
        }

        //Empty from ClientContacts
        public async Task DeleteContactClientsAsync(Guid contactId)
        {
            var clientContacts = _dbContext?.ClientContacts.Where(x => x.ContactId == contactId).ToList();
            if (clientContacts is not null)
            {
                _dbContext?.ClientContacts.RemoveRange(clientContacts);
                await SaveChangesAsync();
            }
        }

        //Gets all contacts
        public IEnumerable<GetContactDTO> GetAll(ListParameters listParams)
        {
            var contacts = _dbContext?.Contacts
                           .OrderBy(x => x.Name)
                           .GroupJoin(_dbContext.ClientContacts, // Join Contacts with ClientContacts
                               cnts => cnts.ContactId, // Contact key
                               clcnts => clcnts.ContactId,  // ClientContact key
                               (cnts, clcnts) => new GetContactDTO
                               {
                                   ContactId = cnts.ContactId,
                                   Name = cnts.Name,
                                   Surname = cnts.Surname,
                                   Email = cnts.Email,
                                   TotalClients = clcnts.Count()
                               })
                                .Skip((listParams.PageNumber - 1) * listParams.PageSize)
                                .Take(listParams.PageSize)
                                .ToList();
            if (contacts is null)
                throw new DatabaseOperationException("Unable to get contacts please try later", "Operation Failed");

            return contacts;



        }

        public IEnumerable<GetContactDTO> GetAllContactsByClientId(Guid clientId)
        {
            var contacts = new List<GetContactDTO>();
            var client = _dbContext?.Clients.FirstOrDefault(x => x.ClientId == clientId);
            
            if(client is null)
                throw new DatabaseOperationException("Unable to find this client. Please try later", "Operation Failed");
            
                

                contacts = _dbContext?.Contacts
                                .OrderBy(x => x.Name)
                                .AsEnumerable()
                                .Join(_dbContext.ClientContacts.Where(x => x.ClientId == clientId).ToList(),
                                            cnts => cnts.ContactId,
                                            ccts => ccts.ContactId,
                                            (cnts, ccts) => {
                                                var count = _dbContext.ClientContacts.Where(x => x.ContactId == cnts.ContactId).Count();
                                               return new GetContactDTO
                                                {
                                                    ContactId = cnts.ContactId,
                                                    Name = cnts.Name,
                                                    Surname = cnts.Surname,
                                                    Email = cnts.Email,
                                                    TotalClients = count
                                                };
                                            }).ToList();
            
            if (contacts is null)
                throw new DatabaseOperationException("Unable to get contacts please try later", "Operation Failed");

            return contacts;


        }

        //Links a contact to a client
        public async Task<bool> LinkToClientAsync(Guid contactId, Guid clientId)
        {
            var isLinked = false;
            var _contact = _dbContext?.ClientContacts.Where(x => x.ContactId == contactId && x.ClientId == clientId).FirstOrDefault();
            if (_contact is null)
            {
                var clientContact = new ClientContact
                {
                    ContactId = contactId,
                    ClientId = clientId
                };
                _dbContext?.ClientContacts.Add(clientContact);
                isLinked = true;
            }
            else
            {
               _dbContext?.ClientContacts.Remove(_contact); //Remove the link if it already exists
                isLinked = false;
            }
           await SaveChangesAsync();
            return isLinked;

        }

        //Updates a contact
        public async Task UpdateAsync(UpdateContactDTO contact)
        {
            var _contact = _dbContext?.Contacts.FirstOrDefault(x => x.ContactId == contact.ContactId);
            if(_contact is not null)
            {
                _contact.Name = contact.Name;
                _contact.Surname = contact.Surname;
                _contact.Email = contact.Email;
                _dbContext?.Contacts.Update(_contact);
                await SaveChangesAsync();
            }
            else
            {
                throw new DatabaseOperationException("Unable to update contact please try later", "Operation Failed");
            }
        }
    }
}
