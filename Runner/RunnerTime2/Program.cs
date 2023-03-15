namespace RunnerTime2
{
    internal class Program
    {
        static void Main()
        {
            string input = "int x = 42; string s = \"hello world\"; int[] a = {1, 2, 3};";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();

            foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }

            Parser parser = new Parser(tokens);
            ASTNode node = parser.Parse();
            Console.WriteLine(node);
            
        }
    }
}