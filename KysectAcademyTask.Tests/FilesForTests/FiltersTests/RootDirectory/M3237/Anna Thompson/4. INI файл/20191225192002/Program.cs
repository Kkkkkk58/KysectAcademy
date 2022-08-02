using System;
using OOP_LABS.LABA_4.Parser;
using System.IO;

namespace OOP_LABS.LABA_4
{
    class Program
    {
        private static Model.IniData ini = null;
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }

        static void Go()
        {
            try
            {
                ini = (new IniDataParser()).ParseFile("../../../ini.ini");
            }
            catch(Exception ex)
            {
                PrintError(ex.Message);
                return;
            }
            
            Menu();
            /*var a = ini["NCMD"]["EnableChannelControl"].GetValue<int>();
            int c = a + 1;
            var b = ini["LEGACY_XML"]["ListenTcpPort"].GetValue<decimal>();
            var d = 0;*/
        }

        static void Menu()
        {
            Console.WriteLine("1. Show sections names");
            Console.WriteLine("2. Show section propertys names");
            Console.WriteLine("3. Get property");

            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        ShowSectionsNames();
                        break;
                    case 2:
                        ShowSectionPropertysNames();
                        break;
                    case 3:
                        ShowProperty();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                PrintError(ex.Message);
            }         

            Menu();
        }

        static void ShowSectionsNames()
        {
            ini.Sections.Show();
        }

        static void ShowSectionPropertysNames()
        {
            var name = GetSectionName();            
            ini[name].Show();
        }

        static void ShowProperty()
        {
            var sectionName = GetSectionName();
            var propertyName = GetPropertyName();
            if (!ini[sectionName].ContainsKey(propertyName))
                throw new Exception("Invalid property name");

            Console.WriteLine("1. String");
            Console.WriteLine("2. Int32");
            Console.WriteLine("3. Decimal");

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Console.WriteLine(ini[sectionName][propertyName].GetValue<string>());
                    break;
                case 2:
                    Console.WriteLine(ini[sectionName][propertyName].GetValue<int>());
                    break;
                case 3:
                    Console.WriteLine(ini[sectionName][propertyName].GetValue<decimal>());
                    break;
                default:
                    break;
            }
        }

        static string GetSectionName()
        {
            Console.Write("Section name - ");
            var name = Console.ReadLine();
            if (ini[name] == null)
                throw new Exception("Invalid section name");
            return name;
        }

        static string GetPropertyName()
        {
            Console.Write("Property name - ");
            return Console.ReadLine();
        }

        static void PrintError(string ex)
        {
            Console.WriteLine($"Error: {ex}");
        }
    }
}
