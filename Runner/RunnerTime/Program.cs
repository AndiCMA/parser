namespace RunnerTime
{
    internal class Program
    {
        
        static void Main(string[] args)
        {

            string input = "if(x > 0) { y = 1;} ; a = 'test'";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }

            Parser parser = new Parser(tokens);
            parser.Parse();

        }
    }
}