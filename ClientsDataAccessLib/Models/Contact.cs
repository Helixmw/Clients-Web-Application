using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Models
{
    public class Contact
    {
        public Guid ContactId { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public ICollection<ClientContact>? ClientContacts { get; set; }


    }
}
