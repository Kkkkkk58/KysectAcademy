using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Lab2 {
    public class Loader {
        public List<Unit> ToLoad()
        {
            List<Unit> units = new List<Unit>();
            foreach (var path in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")) {
                Console.WriteLine(path);
                Assembly asm = Assembly.LoadFrom(path);
                Type[] types = asm.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(Unit)) && !type.IsAbstract)
                    {
                        object u = Activator.CreateInstance(type);
                        units.Add((Unit)u);
                    }
                }
            }
            
            var asmDefault = Assembly.Load("Lab2");
            Type[] defaultTypes = asmDefault.GetTypes();
            foreach(Type type in defaultTypes)
            {
                if (type.IsSubclassOf(typeof(Unit)) && !type.IsAbstract)
                {
                    object u = Activator.CreateInstance(type);
                    units.Add((Unit)u);
                }
            }
            return units;
        }
    }
}