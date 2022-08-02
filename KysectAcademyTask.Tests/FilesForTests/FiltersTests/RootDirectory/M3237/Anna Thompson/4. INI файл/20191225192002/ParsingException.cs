using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_LABS.LABA_4.Exсeptions
{
    public class ParsingException : Exception
    {
        public int LineNumber { get; set; }
        public string LineContents { get; set; }

        public ParsingException(string msg)
           : this(msg, 0, string.Empty, null)
        { }

        public ParsingException(string msg, int lineNumber)
            : this(msg, lineNumber, string.Empty, null)
        { }
        public ParsingException(string msg, int lineNumber, string lineContents)
            : this(msg, lineNumber, lineContents, null)
        { }

        public ParsingException(string msg, int lineNumber, string lineContents, Exception innerException)
            : base(
                string.Format(
                    "{0} при извличении данных из строки {1}: \'{2}\'",
                    msg,
                    lineNumber,
                    lineContents),
                innerException)
        {
            LineNumber = lineNumber;
            LineContents = lineContents;
        }
    }
}
