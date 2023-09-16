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

        Triangle tri = new Triangle();
        tri.OutputCheck("1", "2", "5");
        tri.OutputCheck("5", "2", "5");
        tri.OutputCheck("1", "1", "1");
        tri.OutputCheck("-1", "2", "5");
        tri.OutputCheck("1", "-1", "1");
        tri.OutputCheck("1", "2", "-5");
        tri.OutputCheck("1w", "1", "1");
        tri.OutputCheck("1", "2e", "5");

        Log.CloseAndFlush();
    }
}
