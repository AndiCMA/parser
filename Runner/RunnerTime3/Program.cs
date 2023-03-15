namespace RunnerTime3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "int x = 42; string s = \"hello world\";if( x == 4) { if( x == 3){x=4;} x = 5; x = 2;}";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();

      /*      foreach (Token token in tokens)
            {
                Console.WriteLine(token);
            }
            Console.Write("\n");
            Console.Write("\n");*/

            Parser a = new Parser(tokens);
            /*int i = 0;
            foreach (Block block in a.blocks)
            {
                Console.WriteLine("Block " + i);
                i++;
                foreach (Token token in block.tokens)
                {
                    Console.Write(token.Value);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }*/
            a.run();
        }
    }
}