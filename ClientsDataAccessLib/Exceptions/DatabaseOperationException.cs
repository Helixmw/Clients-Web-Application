using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsDataAccessLib.Exceptions
{
    public class DatabaseOperationException : Exception
    {
        public string? Caption { get; private set; }

        public DatabaseOperationException(string message, string? caption = null):base(message)
        {
            Caption = caption;
        }
        
    }
}
