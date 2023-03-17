using System.Diagnostics;
using System.Text.RegularExpressions;
using Runner.NsParser;
using Runner.NsRunTime;

namespace Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"../../../input.txt");
            
            Parser parser = new Parser(input);

            parser.Run();

            var a = RunTimeVariables.Variables;



            /*  foreach (var block in blocks)
             {
                 System.Console.WriteLine(block.Type);
                 System.Console.WriteLine(block.Value);
             }*/


        }


        private void startTimer()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            long elapsedTimeMs = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Elapsed time: {elapsedTimeMs} ms");
        }
    }
}