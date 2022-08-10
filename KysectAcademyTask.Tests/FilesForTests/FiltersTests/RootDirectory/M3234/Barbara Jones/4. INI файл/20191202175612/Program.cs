using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    public class Configuration
    {
        public Dictionary<string, Dictionary<string, string>> Sections;

        public Configuration()
        {
            Sections = LoadSections();
        }

        public string get_value(string sct, string par, char tp)
        {
            Dictionary<string, string> section = new Dictionary<string, string>();
            try
            {
                section = Sections[sct];
            }
            catch
            {
                throw new Exception("Section was not found");
            }

            string ans;

            try
            {
                ans = section[par];
            }
            catch
            {
                throw new Exception("Parameter was not found");
            }


            int ti;
            double td;

            if (tp == 'I')
                if (Int32.TryParse(ans, out ti))
                    return ans;
                else
                    throw new Exception("The type was not specified correctly");

            if (tp == 'R')
                if (Double.TryParse(ans, out td))
                    return ans;
                else
                    throw new Exception("The type was not specified correctly");

            return ans;
        }

        public Dictionary<string, Dictionary<string, string>> LoadSections()
        {
            Dictionary<string, Dictionary<string, string>> loadedsections = new Dictionary<string, Dictionary<string, string>>();

            try
            {
                using (StreamReader sr = new StreamReader(@"config.txt"))
                { }
            }
            catch
            {
                throw new Exception("File <config.txt> not found");
            }

            using (StreamReader sr = new StreamReader(@"config.txt"))
            {
                string line;
                string curnm = "";
                Dictionary<string, string> curlst = new Dictionary<string, string>();

                while ((line = sr.ReadLine()) != null)
                {
                    int i = 0;

                    while (i < line.Length && line[i] != ';') i++;
                    if (i < line.Length) line = line.Remove(i);
                    while (line != "" && line[line.Length - 1] == ' ') line = line.Remove(line.Length - 1);

                    if (curnm == "")
                    {
                        if (line != "" && (line[0] != '[' || line[line.Length - 1] != ']'))
                            throw new Exception("Config file is not built correctly");
                        else
                            for (i = 1; i < line.Length - 1; i++)
                            {
                                if (restricted_symbol(line[i]))
                                    throw new Exception("Section name contains restricted symbols");

                                curnm += line[i];
                            }

                        //Console.WriteLine(line + "S");
                        continue;
                    }

                    if (line == "")
                    {
                        //Console.WriteLine(line + " ");
                        loadedsections.Add(curnm, curlst);
                        curnm = "";
                        curlst = new Dictionary<string, string>();
                        continue;
                    }

                    if (line.Contains(" = "))
                    {
                        i = 0;
                        string prmnm = "";
                        string prmvl = "";

                        while (line[i] != ' ' || line[i + 1] != '=' || line[i + 2] != ' ')
                        {
                            if (restricted_symbol(line[i]))
                                throw new Exception("Parameter name contains restricted symbols");

                            prmnm += line[i];
                            i++;
                        }

                        i += 3;
                        while (i < line.Length)
                        {
                            if (line[i] == ' ')
                                throw new Exception(line.Substring(i)); //"Parameter value contains space" + 

                            prmvl += line[i];
                            i++;
                        }

                        //Console.WriteLine(prmnm + ":" + prmvl);
                        curlst.Add(prmnm, prmvl);
                        continue;
                    }
                    else
                        throw new Exception("Config file is not built correctly");
                }

                if (curnm != "")
                {
                    //Console.WriteLine(" Complete " + curlst.Count);

                    loadedsections.Add(curnm, curlst);
                    //Console.WriteLine(loadedsections[0].Parameters.Count);

                }
            }

            return loadedsections;
        }

        public bool restricted_symbol(char x)
        {
            bool t = true;
            if ((x >= '0' && x <= '9') || (x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z') || (x == '_'))
                t = false;

            return t;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Configuration is loading...");
            Configuration config = new Configuration();
            Console.WriteLine("Success.");


            /*for (int i = 0; i < config.Sections.Count; i++)
            {
                Console.WriteLine(config.Sections[i].name);

                Console.WriteLine(config.Sections[i].Parameters.Count);
                for (int j = 0; j < config.Sections[i].Parameters.Count; j++)
                    Console.WriteLine(">" + config.Sections[i].Parameters[j].name);

            }*/

            while (true)
            {
                string section, name, tpe;
                char tp;
                Console.WriteLine("Write parameter specifications");
                Console.WriteLine(">Section");
                Console.Write(">");
                section = Console.ReadLine();
                Console.WriteLine(">Name");
                Console.Write(">");
                name = Console.ReadLine();
                Console.WriteLine(">Type");
                Console.Write(">");
                tpe = Console.ReadLine();
                if (tpe != "") tp = tpe[0];
                else tp = 'S';
                

                string ans = config.get_value(section, name, tp);
                Console.WriteLine(ans);

                Console.WriteLine("type <stop> to terminate, anything else to continue");
                string s = Console.ReadLine();
                if (s == "stop") break;
            }
        }
    }
}

