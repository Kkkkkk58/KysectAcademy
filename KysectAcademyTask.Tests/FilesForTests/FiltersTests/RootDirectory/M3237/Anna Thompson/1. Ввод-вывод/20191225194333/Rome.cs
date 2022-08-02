using System;
using System.Collections.Generic;
using LABA_1.Exceptions;

namespace LABA_1
{
    public class Rome
    {
        private static Dictionary<string, int> convert = new Dictionary<string, int>()
        {
            {"I",   1},
            {"V",   5},
            {"X",   10},
            {"L",   50},
            {"C",   100},
            {"D",   500},
            {"M",   1000}
        };

        public static int ConvertToInt32(string str)
        {
            if (!convert.ContainsKey(str[str.Length - 1].ToString()))
                throw new Exception("No valid input");
            int sum = convert[str[str.Length - 1].ToString()];
            int last_num = sum;
            int value_repeat = 1;
            bool last_is_combination = false;
            for (var i = str.Length - 2; i >= 0; i--)
            {
                if (!convert.ContainsKey(str[i].ToString()))
                    throw new Exception("No valid input");
                var num = convert[str[i].ToString()];
                if (num == last_num)
                {
                    value_repeat++;
                    if (value_repeat >= 4)
                        throw new Exception("No valid input");
                }
                else
                {
                    value_repeat = 1;
                }
                if (num < last_num)
                {
                    if (last_is_combination)
                        throw new Exception("No valid input");
                    if (str[i + 2] == str[i])
                        throw new Exception("No valid input");
                    sum -= num;
                    last_is_combination = true;
                }
                else
                {
                    sum += num;
                    last_is_combination = false;
                }
                last_num = num;
            }

            return sum;
        }
    }
}
