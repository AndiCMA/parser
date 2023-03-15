using Runner.Parser;
using System.Text.RegularExpressions;

namespace Runner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"../../../input.txt");

            var lexer = new Lexer(input);
            
            List<Block> blocks = lexer.Blockenize();
            foreach (var block in blocks)
            {
                System.Console.WriteLine(block.Type);
            }
            
            
        }
    }
}