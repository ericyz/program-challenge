using System;

namespace LovelyCats.Directory
{
    public class Program
    {
        static void Main(string[] args)
        {
            var startUpTask = new Startup();

            startUpTask.RunAsync().Wait();
            Console.WriteLine("Please press enter to exit");
            Console.ReadLine();
        }
    }
}
