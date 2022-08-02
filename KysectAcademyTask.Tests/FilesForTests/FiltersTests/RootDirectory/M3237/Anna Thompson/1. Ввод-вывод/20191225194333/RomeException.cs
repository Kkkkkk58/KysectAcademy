using System;
namespace LABA_1.Exceptions
{
    public class RomeException : Exception
    {
        public RomeException(string msg, Exception innerExeption)
             : base(
                  $"RomeException: {msg}",
                  innerExeption)
        { }
    }
}
