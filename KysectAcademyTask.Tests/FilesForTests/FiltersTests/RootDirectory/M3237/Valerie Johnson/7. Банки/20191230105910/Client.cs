using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab7
{
    public class Client
    {
        public Guid ClientId { get; }
        [Required(ErrorMessage = "Last name must be set")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "First name must be set")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
        public Client()
        {
            ClientId = Guid.NewGuid();
        }
    }
}
