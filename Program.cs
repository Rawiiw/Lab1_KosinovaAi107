using System;
using Serilog;

class Program
{
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Console.WriteLine("Введите сторону a");
        string a = Console.ReadLine();
        Console.WriteLine("Введите сторону b");
        string b = Console.ReadLine();
        Console.WriteLine("Введите сторону c");
        string c = Console.ReadLine();

        Triangle tri = new Triangle();
        tri.OutputCheck(a, b, c);

        Log.CloseAndFlush();
    }
}
