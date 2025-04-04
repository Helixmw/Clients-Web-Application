using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Models
{
    public class Client
    {
        public Guid ClientId { get; set; }

        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
       
    }
}
