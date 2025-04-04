using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.DTOs.Clients
{
    public class AddClientDTO
    {
        [Required(ErrorMessage = "Please enter your client name")]
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;
    }
}
