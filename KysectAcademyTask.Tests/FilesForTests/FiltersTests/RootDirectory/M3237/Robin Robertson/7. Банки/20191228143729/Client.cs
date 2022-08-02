using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using OOPLab7.Accounts;

namespace OOPLab7
{
    class Client
    {
        public Client()
        {            
            ClientId = Guid.NewGuid();
        }
        public Guid ClientId { get; }
        [Required(ErrorMessage = "First name must be set")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name must be set")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
        

    }
}
