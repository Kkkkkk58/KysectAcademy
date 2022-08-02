using System;
namespace LABA_5
{
    public static class Logger
    {
        private static readonly object _writeLock = new object();

        public enum Mode
        {
            Info,
            Warn,
            Error
        }

        private static void Write(string text, Mode mode)
        {
            lock (_writeLock)
            {
                Console.WriteLine(text);
            }
        }

        private static void Write(string text, ConsoleColor color, Mode mode)
        {
            lock (_writeLock)
            {
                Console.ForegroundColor = color;
                Write(text, mode);
                Console.ResetColor();
            }
        }

        public static void Info(string message)
        {
            Write(message, ConsoleColor.Blue, Mode.Info);
        }

        public static void Warn(string message)
        {
            Write(message, ConsoleColor.Yellow, Mode.Warn);
        }

        public static void Error(string message)
        {
            Write(message, ConsoleColor.Red, Mode.Error);
        }
    }
}
