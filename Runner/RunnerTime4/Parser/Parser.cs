using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Parser
{
    public List<Block> blocks;

    public Parser()
    {
        blocks = new List<Block>();
    }
    public Parser(List<Token> tokens)
    {
        blocks = new List<Block>();
        Block block = new Block();
        int depth = 0;
        foreach (Token token in tokens)
        {
            if (token.Type == TokenType.DelimiterLine && token.Value == ";" && depth == 0)
            {
                block.blockType = BlockType.Exec;
                blocks.Add(block);
                block = new Block();
            }
            else if (token.Type == TokenType.DelimiterBody && token.Value == "{")
            {
                depth++;
                block.tokens.Add(token);
            }
            else if (token.Type == TokenType.DelimiterBody && token.Value == "}")
            {
                block.tokens.Add(token);
                if (depth == 1)
                {
                    block.discoverType();
                    blocks.Add(block);
                    block = new Block();
                }
                depth--;
            }
            else
            {
                block.tokens.Add(token);
            }
        }
    }

    public void Compile()
    {
        Console.WriteLine("\n");
        foreach (Block block in blocks)
        {
            foreach (Token token in block.tokens)
            {
                Console.Write(token.Value + " ");
            }
            Console.WriteLine("\n");
        }
    }

    public void run()
    {
        foreach (Block currentBlock in blocks)
        {
            //currentBlock.run();
        }
    }
}

