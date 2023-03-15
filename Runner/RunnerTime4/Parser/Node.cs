using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum NodeTypes
{
    ProceduralNode,
    ConditionalNode,
    LoopNode
}

interface Node
{
    public int line { get; set; }
    public Node? next { get; set; }
    Block? block { get; set; }
    object? Execute();
}

class MainNode : Node
{
    public int line { get; set; }
    public Node? next { get; set; }
    public Block? block { get; set; }
    public MainNode()
    {
        next = null;
        block = null;
    }
    public object Execute()
    {
        Console.WriteLine("Main procedure started");
        return true;
    }
}
    
class ProceduralNode : Node
{
    public int line { get; set; }

    public Block? block
    {
        get;
        set;
    }
    public Node? next { 
        get; 
        set; 
    }
    
    public ProceduralNode(Block block)
    {
        this.block = block;
        next = null;
    }

    public object Execute()
    {
        RunTimeWrapper runTimeWrapper = new RunTimeWrapper(block.tokens);
        Console.WriteLine("Executing ProceduralNode " + block.ToString());
        return true;
    }

    public override string ToString()
    {
        return "Line " + line + ": " + block.ToString();
    }

}

class ConditionalNode : Node
{
    public int line { get; set; }
    public Block? block
    {
        get;
        set;
    }
    public Node? next
    {
        get;
        set;
    }
    public Node? trueNext
    {
        get;
        set;
    }
    public Node? elseNext
    {
        get;
        set;
    }

    public ConditionalNode(Block block)
    {
        this.block = block;
        next = null;
        elseNext = null;
    }

    public object Execute()
    {
        Console.WriteLine("Executing ProceduralNode " + block.ToString());
        return true;
    }

    public override string ToString()
    {
        return "Line " + line + ": " + block.ToString();
    }
}

class LoopNode : Node
{
    public int line { get; set; }
    public Block? block
    {
        get;
        set;
    }
    public Node? next
    {
        get;
        set;
    }
    public Node? repeat
    {
        get;
        set;
    }

    public LoopNode(Block block)
    {
        this.block = block;
        next = null;
    }

    public object Execute()
    {
        Console.WriteLine("Executing LoopNode " + block.ToString());
        Random rnd = new Random();
        int rando = rnd.Next(0, 100);
        if (rando > 50)
        {
            return true;
        }
        else
        {
            return false;
        }
        return true;
    }

    public override string ToString()
    {
        return "Line " + line + ": " + block.ToString();
    }
}
