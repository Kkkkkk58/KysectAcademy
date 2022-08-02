using System;
using System.IO;
using LABA_1.Exceptions;
namespace LABA_1
{
    public class Adder
    {
        public Adder()
        { }

        public static void Go()
        {
            //var location_input = "../../../1. Adder/input.txt";
            var location_input = "./1. Adder/input.txt";
            //var location_output = "../../../1. Adder/output.txt";
            var location_output = "./1. Adder/output.txt";

            if (!File.Exists(location_input))
                throw new AdderException("Not fount input file", null);

            var str = File.ReadAllLines(location_input);
            double sum = 0;
            for (var i = 0; i < str.Length; i++)
            {
                try
                {
                    sum += System.Convert.ToDouble(str[i]);
                }
                catch (Exception ex)
                {
                    throw new AdderException($"Convert error, line - {i + 1}", ex);
                }
            }
            File.WriteAllText(location_output, sum.ToString());
        }
    }
}
