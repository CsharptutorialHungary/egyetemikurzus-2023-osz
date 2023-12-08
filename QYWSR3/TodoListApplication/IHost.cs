using System;

namespace ToDoListApplication
{
    public interface IHost
    {
        void WriteLine(string format, params object[] parameters)
        {
            Console.WriteLine(format, parameters);
        }
        void Write(string format, params object[] parameters)
        {
            Console.Write(format, parameters);
        }
        void Clear()
        {
            Console.Clear();
        }
        void Exit(int exitCode = 0)
        {
            Environment.Exit(exitCode);
        }
        string ReadLine()
        {
            string? line = Console.ReadLine();
            return line ?? string.Empty;
        }
    }
}