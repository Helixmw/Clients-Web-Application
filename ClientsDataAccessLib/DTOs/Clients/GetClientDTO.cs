using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.DTOs.Clients
{
    public class GetClientDTO
    {
      
        public Guid ClientId { get; set; }
      
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public int TotalContacts { get; set; }
    }
}
