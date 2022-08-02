using System;
using System.Collections.Generic;
using LABA_1.Exceptions;

namespace LABA_1
{
    public class Fibonacci
    {
        public Fibonacci()
        {
        }

        public static void show(int n)
        {
            if (n < 0)
                throw new FibonacciException("Value must be possitive", null);
           
            List<int> nums = new List<int>() { 0, 1, 1 };
            for (var i = 3; i < n; i++)
                nums.Add(nums[nums.Count - 1] + nums[nums.Count - 2]);

            for (var i = 0; i < n; i++)
                Console.Write(nums[i] + " ");
        }
    }
}
