using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Client
    {
        public string ID;
        public string Name;
        public string Surname;
        public string Address;
        public string Passport;

        public Client(string NM, string SNM, string ADD, string PSS, BankInformation BI)
        {
            ID = BI.AvailibleClientID.ToString();
            Name = NM;
            Surname = SNM;
            Address = ADD;
            Passport = PSS;
        }

        public bool istrustful()
        {
            if (Address == "" || Passport == "")
                return false;
            else
                return true;
        }
    }
}
