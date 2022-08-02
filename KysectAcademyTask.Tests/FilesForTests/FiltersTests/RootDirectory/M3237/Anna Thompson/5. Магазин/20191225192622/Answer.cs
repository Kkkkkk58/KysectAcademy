using System;
namespace LABA_5
{
    public class Answer<T>
    {
        public string Error { get; set; }
        public T Data { get; set; }
        public bool Success => string.IsNullOrEmpty(Error);
    }
}
