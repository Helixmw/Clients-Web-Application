using ClientsDataAccessLib.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientsAppUI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ClientContact> ClientContacts { get; set; }

    }
}
