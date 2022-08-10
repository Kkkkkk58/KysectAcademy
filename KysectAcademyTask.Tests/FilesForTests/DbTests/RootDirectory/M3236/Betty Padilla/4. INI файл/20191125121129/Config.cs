using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.lab4
{
	class Config
	{
		Dictionary<string, Dictionary<string, string>> map = new Dictionary<string, Dictionary<string, string>>();

		public Config(string filepath)
		{
			try
			{
				using (StreamReader sr = new StreamReader(filepath))
				{
					string line;
					Dictionary<string, string> section = null;

					Char[] delim = { ' ', '='};


					while ((line = sr.ReadLine()) != null)
					{

						if (line.Length == 0)
							continue;

						if (line[0] == '[')
						{
							string title = "";
							int i = 1;

							while (i < line.Length)
							{
								if (Char.IsLetterOrDigit(line[i]))
									title += line[i];
								else if (line[i] == ']')
									break;
								else
									throw new FormatException(line + "\n Wrong section format");
								i++;
							}

							if (!map.TryGetValue(title, out section))
							{
								section = new Dictionary<string, string>();
								map.Add(title, section);
							}
						}

						else if (line[0] != '\n' && line[0] != ' ' && line[0] != ';')
						{
							String[] tokens = line.Split(delim, 2, StringSplitOptions.RemoveEmptyEntries);
							if (tokens.Length < 2)
								throw new FormatException(line);

							foreach (char c in tokens[0])
								if (c == ';')
									throw new FormatException(line);

							if (tokens[1][0] == ';')
								throw new FormatException(line);

							for (int i = 1; i < tokens[1].Length; i++)
								if (tokens[1][i] == ';')
								{
									tokens[1] = tokens[1].Substring(0, i);
									break;
								}
							//Console.WriteLine(tokens[0]);
							//Console.WriteLine(tokens[1]);

							if (section == null)
								throw new FormatException(line + "\n Not in any section");

							section.Add(tokens[0], tokens[1]);
						}
					}
				}
			}

			catch (Exception e)
			{
				if (e is FileNotFoundException)
					Console.WriteLine("File " + filepath + " not found");
				else if (e is FormatException)
					Console.WriteLine("Wrong format: " + e.Message);
				else
					throw e;
			}
		}

		public bool Find<T>(string section, string name, out T value)
		{
			if (map.TryGetValue(section, out var dict))
			{
				if (dict.TryGetValue(name, out string s))
				{
					try
					{
						value = (T)Convert.ChangeType(s, typeof(T));
					}
					catch (FormatException e)
					{
						Console.WriteLine("Can not convert " + s + " to type " + typeof(T));
						value = default(T);
						return false;
					}
					return true;
				}
				value = default(T);
				return false;
			}
			value = default(T);
			return false;

		}
	}
}
