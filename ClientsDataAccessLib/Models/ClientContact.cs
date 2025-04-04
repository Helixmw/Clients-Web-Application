using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Models
{
    public class ClientContact
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(Contact))]
        public Guid ContactId { get; set; }

        public Client Client { get; set; } = null!;

        public Contact Contact { get; set; } = null!;


    }
}
