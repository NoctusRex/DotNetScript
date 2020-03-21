using System;

namespace DotNetScriptbase.Helper
{
    public static class ConsoleHelper
    {
        private static ConsoleColor OriginalConsoleColor { get; set; }

        public static void Init() => OriginalConsoleColor = Console.ForegroundColor;

        public static void WriteLine(string text) => Console.WriteLine(text);

        public static void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            WriteLine(text);

            Console.ForegroundColor = OriginalConsoleColor;
        }

        public static void WriteError(Exception ex)
        {
#if DEBUG
            WriteError(ex.ToString());
#else
            WriteError(ex.Message);
#endif
        }

        public static void WriteWarning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            WriteLine(text);

            Console.ForegroundColor = OriginalConsoleColor;
        }

        public static void NewLine()
        {
            WriteLine("");
        }

    }
}
