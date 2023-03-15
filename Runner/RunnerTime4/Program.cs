namespace RunnerTime4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "int x = 42; " +
                "string s = \"hello world\";" +
                "if( x == 4) " +
                "{ " +
                "if( x == 3)" +
                "{" +
                "x = 4;" +
                "} " +
                "x = 5; x = 2;} " +
                "while( x == 2) " +
                "{ " +
                "x = 3;" +
                " s = 'test';" +
                "} " +
                "int d = 782; ";
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();
            Parser parser = new Parser(tokens);

            
            NodeWrapper.Compile(parser.blocks);
            
            NodeWrapper.Execute();

            var a = RunTimeData.variables;


            /*Node initial = new ProceduralNode(parser.blocks[0]);
            Node current;
            current = initial;

            current.next = new ProceduralNode(parser.blocks[1]);
            current = current.next;
            

            current.next = new ConditionalNode(parser.blocks[2]);

            current = initial;
            while (current != null)
            {
                Console.WriteLine(current.ToString());
                current = current.next;
            }*/


        }
    }
}