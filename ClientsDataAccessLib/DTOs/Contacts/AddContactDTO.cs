using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.DTOs.Contacts
{
    public class AddContactDTO
    {
        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter a Surname")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Please enter an Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address")]
        public string Email { get; set; } =  null!;


    }
}
