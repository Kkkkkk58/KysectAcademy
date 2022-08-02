using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LABA_3
{
    public class Artist
    {
        public string name { get; set; }

        public Artist (string Name)
        {
            name = Name;
        }

        public static bool operator ==(Artist x, string Name)
        {
            return ((object)x != null) ? x.name == Name : false;
        }

        public static bool operator !=(Artist x, string Name)
        {
            return ((object)x != null) ? x.name != Name : (Name == null) ? false : true;
        }
    }
}
