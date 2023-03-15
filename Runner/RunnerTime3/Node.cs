using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface Node
{
    void run();
}

class ExecuteNode : Node
{
    public List<Token> tokens;
    public ExecuteNode(List<Token> tokens)
    {
        this.tokens = tokens;
    }
    public void run()
    {
        Console.WriteLine("Exec Node : ");
        foreach(Token token in tokens)
        {
            Console.Write(token.Value);
            Console.Write(" ");
        }
        Console.Write("\n");
    }
}
class ConditionalNode : Node
{
    public List<Token> condition;

    public List<Token> body;

    private Parser parser;
    
    public ConditionalNode(List<Token> condition, List<Token> body)
    {
        this.condition = condition;
        this.body = body;
        parser = new Parser(body);
    }
    public void run()
    {
        Random r = new Random();
        int rInt = r.Next(0, 100);
        if (rInt < 40)
        {
            parser.run();
        }
        
    }
}
class WhileNode : Node
{
    public Block condition;
    
    public List<Token> tokens;
    public WhileNode(List<Token> tokens)
    {
        this.tokens = tokens;
    }
    public void run()
    {
        Console.WriteLine("Hello, World!");
    }
}
class ForNode : Node
{
    public Block condition;

    public List<Token> tokens;
    public ForNode(List<Token> tokens)
    {
        this.tokens = tokens;
    }
    public void run()
    {
        Console.WriteLine("Hello, World!");
    }
}