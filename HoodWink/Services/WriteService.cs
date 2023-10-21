using System;

namespace HoodWink.Services
{
    public static class WriteService
    {
        public static void Success(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[+] Success: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void Suggestion(string suggestion, string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[+] Suggested {suggestion}: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void Progress(string name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[+] Building: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);
            Console.ResetColor();
        }

        public static void Error(string err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[!] Error: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(err);
            Console.ResetColor();
        }

        public static void ErrorExit(string err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[!] Error: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(err);
            Console.ResetColor();
            System.Environment.Exit(0);
        }
    }
}