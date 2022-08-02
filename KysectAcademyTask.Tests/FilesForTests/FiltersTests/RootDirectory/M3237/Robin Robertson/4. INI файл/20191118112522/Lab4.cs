using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifmolab
{
    class Lab4
    {
        private Dictionary<String, Section> sections;
        public Lab4()
        {
            sections = new Dictionary<string, Section>();
            Dictionary<String, Type> types=new Dictionary<string, Type>();
            types.Add("double", typeof(double));
            types.Add("String", typeof(String));
            types.Add("int", typeof(int));
            String nameFile;
            if (!CreateFile(out nameFile))
            {
                return;
            }
            if (!IsCorrectFileFormat(nameFile))
            {
                Console.WriteLine("Файл имеет неверный формат");
                return;
            }
            String s;
            while (true) {
                Console.WriteLine("Напишите через пробел тип параметра, название параметра и название секции или 0, чтобы закончить");
                s = Console.ReadLine();
                if (s == "0")
                {
                    return;
                }
                String[] arrayS = s.Split(new char[] { ' ' }).ToArray();
                
                if (sections[arrayS[2]] == null)
                {
                    Console.WriteLine("нет такой секции");
                    continue;
                }
                Section section = sections[arrayS[2]];
                object param;
                Type t = types[arrayS[0]];
                if (!section.TryGetObject(arrayS[1]))
                {
                    Console.WriteLine("нет такого параметра");
                    continue;
                }
                param = section.GetObject(arrayS[1]);
                if (param.GetType() != types[arrayS[0]])
                {
                    Console.WriteLine("не соотвествие типов");
                    continue;
                }
                Console.WriteLine(param);
            }
        }

        bool CreateFile(out String nameFile)
        {
            Console.WriteLine("Напишите название файла: ");
            while (true) {
                nameFile = Console.ReadLine();
                if (nameFile == "0")
                {
                    return false;
                }
                if (!File.Exists(nameFile))
                {
                    Console.WriteLine("Напишите корректное название файла или выйдите написав: 0" );
                }
                else
                {
                    return true;
                }
            }
        }
        bool IsCorrectFileFormat(string nameFile)
        {
            using (StreamReader r = new StreamReader(nameFile))
            {
                String s;
                Section section = null;
                while (!r.EndOfStream)
                {
                    s = r.ReadLine();
                    String name;
                    Object value;
                    if (s == "" || s[0] == ';')
                    {
                        if (section != null)
                        {
                            section = null;
                        }
                        continue;
                    }
                    if (s[0] == '[')
                    {
                        if (!GetSection(s, out name))
                        {
                             return false;
                        }
                        if (section != null)
                        {
                            return false;
                        }
                        section = new Section(name);
                        sections.Add(name, section);
                    }
                    if (char.IsDigit(s[0]) || char.IsLetter(s[0]) || s[0] == '_')
                    {

                        if (!GetParameter(s, out name, out value) || section == null)
                        {
                            return false;
                        }
                        section.AddObject(name, value);
                    }
                }
                return true;
            }
        }
        private bool GetSection(String s, out String name)
        {
            String[] arrayS = s.Split(new char[] { ' ' }).ToArray();
            name = "";
            for (int i = 1; i < arrayS[0].Length - 1; i++)
            {
                name += arrayS[0][i];
            }

            if (!CorrectName(name) || !(arrayS[0][arrayS[0].Length - 1] == ']') || (arrayS.Length > 1 && arrayS[1] != ";"))
            {
                return false;
            }
            
            return true;
        }
        private bool GetParameter(String s, out String name, out object value)
        {
            
            String[] arrayS = s.Split(new char[] { ' ' }).ToArray();
            name = arrayS[0];
            int count;
            try
            {
                if (int.TryParse(arrayS[2], out count))
                {
                    value = count;
                }
                else
                {
                    double f;
                    if (FloatParse(arrayS[2], out f))
                    {
                        value = f;
                    }
                    else
                    {
                        value = arrayS[2];
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                value = 1;
                return false;
            }
            if (!CorrectName(arrayS[0]) || arrayS[1]!="=" || (arrayS.Length > 3 && arrayS[3] != ";"))
            {
                return false;
            }
            return true;
        }
        private bool CorrectName(String name)
        {
            foreach(char c in name)
            {
                if (!(char.IsDigit(c) || char.IsLetter(c) || c == '_'))
                {
                    return false;
                }
            }
            return true;
        }
        private bool FloatParse(String s, out double f)
        {
            String[] arrayS = s.Split(new char[] { '.' }).ToArray();
            int a, b;
            f = 0;
            if (arrayS.Length != 2 || !int.TryParse(arrayS[0],out a) || !int.TryParse(arrayS[1], out b))
            {
                return false;
            }
            f = a + b / Math.Pow(10, arrayS[1].Length);
            return true;
        }

    }
}
