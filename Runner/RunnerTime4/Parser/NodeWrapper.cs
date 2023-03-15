using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

static class NodeWrapper
{
    private static Node initial = null;
    private static Node current = null;
    private static Node last = null;
    private static int line = 0;

    public static void Compile(List<Block> blocks)
    {
        if( initial == null)
        {
            initial = new MainNode();
            last = initial;
            current = initial;
        }
        foreach (Block block in blocks)
        {
            if (block.blockType == BlockType.Exec)
            {
                AddProcedural(block);
            }
            else if (block.blockType == BlockType.If)
            {
                AddConditional(block);
            }
            else if (block.blockType == BlockType.While)
            {
                AddWhileLoop(block);
            }
        }
    }

    private static void AddProcedural(Block block)
    {
        current.next = new ProceduralNode(block);
        current.line = line++;
        current = current.next;
        last = current;
    }
    private static void AddConditional(Block block)
    {
        current.next = new ConditionalNode(new Block(block.getConditionalTokens()));
        current.line = line++;
        current = current.next;
        last = current;
        Node backUp = current;
        Parser conditionalParser = new Parser(block.getBodyTokens());
        NodeWrapper.Compile(conditionalParser.blocks);
        current = backUp;
        ((ConditionalNode)current).trueNext = current.next;
        current.next = null;
    }
    private static void AddWhileLoop(Block block)
    {
        current.next = new LoopNode(new Block(block.getConditionalTokens()));
        current.line = line++;
        current = current.next;
        last = current;
        Node backUp = current;
        Parser loopParser = new Parser(block.getBodyTokens());
        NodeWrapper.Compile(loopParser.blocks);
        current = backUp;
        ((LoopNode)current).repeat = current.next;
        current.next = null;
    }


    public static void Execute(Node nod = null)
    {
        current = initial;
        if(nod != null)
        {
            current = nod;
        }
        while (current != null)
        {
         
            if(current.GetType().ToString() == "ProceduralNode" || current.GetType().ToString() == "MainNode")
            {
                current.Execute();
                current = current.next;

            }else if(current.GetType().ToString() == "ConditionalNode")
            {
                Node hold = current;

                if ((bool)current.Execute())
                {
                    current = ((ConditionalNode)current).trueNext;
                    Execute(current);
                }
                else if (((ConditionalNode)current).elseNext != null)
                {
                    current = ((ConditionalNode)current).elseNext;
                    Execute(current);
                }
                current = hold;
                current = current.next;
                
            }else if(current.GetType().ToString() == "LoopNode")
            {
                Node hold = current;
                
                while ((bool)hold.Execute())
                {
                    Execute(((LoopNode)hold).repeat);
                }

                current = hold;
                current = current.next;
            }
            
        }
    }



}

