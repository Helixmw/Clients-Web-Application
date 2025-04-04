using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.DTOs.Clients
{
    public class UpdateClientDTO
    {
       
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "Please enter Client Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter Client Code")]
        public string Code { get; set; } = null!;
    }
}
