using System;
namespace LABA_1.Exceptions
{
    public class FibonacciException : Exception
    {
        public FibonacciException(string msg, Exception innerExeption)
             : base(
                  $"FibonacciException: {msg}",
                  innerExeption)
        { }
    }
}
