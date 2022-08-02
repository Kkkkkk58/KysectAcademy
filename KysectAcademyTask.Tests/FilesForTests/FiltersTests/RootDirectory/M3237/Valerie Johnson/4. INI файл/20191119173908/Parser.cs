using System;
using System.Text.RegularExpressions;

namespace Lab4
{
    class Parser
    {
        public string textfromfile;
        public Parser(string textfromfile)
        {
            this.textfromfile = textfromfile;
        }
        public string DeleteComments()
        {
            textfromfile = Regex.Replace(textfromfile, @";(.)*", string.Empty);
            textfromfile = Regex.Replace(textfromfile, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            return textfromfile;
        }
        public dynamic Find(string section, string param, string type)
        {
            section = "[" + section + "]";
            int start = textfromfile.IndexOf(section);
            if (start == -1)
            {
                return "Секция не найдена";
            }
            start += section.Length;
            int finish = textfromfile.IndexOf('[', start);
            string textfromsection;
            if (finish == -1)
            {
                textfromsection = textfromfile.Substring(start);
            }
            else
            {
                textfromsection = textfromfile[start..finish];
            }
            start = textfromsection.IndexOf(param);
            if (textfromsection.IndexOf(string.Format("\n{0}=", param)) == -1 && textfromsection.IndexOf(string.Format("\n{0} =", param)) == -1)
            {
                return "Параметра нет в данной секции";
            }
            textfromsection = textfromsection.Substring(start);
            textfromsection = Regex.Match(textfromsection, @"=(.)+").Value;
            textfromsection = Regex.Match(textfromsection, @"[^=\s]+").Value;
            if (textfromsection.Length == 0)
            {
                return "Параметра нет в данной секции";
            }
            else
            {
                int int_param;
                double double_param;
                if (type == "string")
                {
                    Int32.TryParse(textfromsection, out int_param);
                    textfromsection = Regex.Replace(textfromsection, @"\.", ",");
                    Double.TryParse(textfromsection, out double_param);
                    if (int_param != 0 || double_param != 0)
                    {
                        return "Неправильный тип параметра";
                    }
                    else
                    {
                        return textfromsection;
                    }
                }
                else if (type == "int")
                {
                    if (Int32.TryParse(textfromsection, out int_param))
                    {
                        return int_param;
                    }
                    else
                    {
                        return "Неправильный тип параметра";
                    }
                }
                else if (type == "double")
                {
                    textfromsection = Regex.Replace(textfromsection, @"\.", ",");
                    if (Double.TryParse(textfromsection, out double_param))
                    {
                        return double_param;
                    }
                    else
                    {
                        return "Неправильный тип параметра";
                    }
                }
            }
            return null;
        }
    }
}
