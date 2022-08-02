using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

namespace Laba_v2
{
    class ModsMan
    {
        public static void GetUnits()
        {
            List<Unit> units = new List<Unit>();
            List<Assembly> data = new List<Assembly>();
            foreach (var item in Directory.GetFiles("mods"))
            {
                data.Add(Assembly.LoadFrom(item));
            }
            foreach (var item in data)
            {
                foreach (var type in item.GetTypes())
                {
                    if (type.BaseType == typeof(Unit))
                    {
                        units.Add(Activator.CreateInstance(type) as Unit);
                    }
                }
            }

            foreach (Unit num in units)
                Global_Unit.Lists.Add(num);
        }
    }
}
