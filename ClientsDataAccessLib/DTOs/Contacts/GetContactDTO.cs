using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.DTOs.Contacts
{
    public class GetContactDTO
    {
        public Guid ContactId { get; set; }

        public string Name { get; set; } = null!;


        public string Surname { get; set; } = null!;


        public string Email { get; set; } = null!;

        public int TotalClients { get; set; }
    }
}
