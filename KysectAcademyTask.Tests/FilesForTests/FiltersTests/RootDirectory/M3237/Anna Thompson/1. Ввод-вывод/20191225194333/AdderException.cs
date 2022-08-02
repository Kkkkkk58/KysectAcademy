using System;
namespace LABA_1.Exceptions
{
    public class AdderException : Exception
    {
        public AdderException(string msg, Exception innerExeption)
            :base(
                 $"AdderException: {msg}",
                 innerExeption)
        {}
    }
}
