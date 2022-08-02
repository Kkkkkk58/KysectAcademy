using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifmolab
{
    class Section
    {
        private String name;
        private Dictionary<String, Object> parametrs;
        public Section(String name)
        {
            this.name = name;
            parametrs = new Dictionary<string, Object>();
        }
        public bool TryGetObject(String key)
        {

            if (parametrs[key] == null)
            {
                return false;
            }
            return true;
        }
        public void AddObject(String key, Object count)
        {
            parametrs[key] = count;
        }
        public String GetName()
        {
            return name;
        }
        public Object GetObject (String key)
        {
                       
            return parametrs[key];
        }
    }
}
