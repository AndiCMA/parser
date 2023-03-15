using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum BlockType
{
    Exec,
    If,
    While,
    For,
    Discover
}
class Block
{
    public List<Token> tokens;
    public BlockType blockType;

    public Block()
    {
        tokens = new List<Token>();
        blockType = BlockType.Discover;
    }
    public Block(List<Token> tokens)
    {
        this.tokens = tokens;
        discoverType();
    }
    public Block(List<Token> tokens, BlockType Type)
    {
        this.tokens = tokens;
        blockType = Type;
    }
    
    public List<Token> getConditionalTokens()
    {
        int pos1 = tokens.FindIndex(a => a.Value == "(");
        int pos2 = tokens.FindIndex(a => a.Value == ")");
        List<Token> returnableTokens = new List<Token>();
        for (int i = pos1 + 1; i < pos2; i++)
        {
            returnableTokens.Add(tokens[i]);
        }
        return returnableTokens;
    }
    public List<Token> getBodyTokens()
    {
        int pos1 = tokens.FindIndex(a => a.Value == "{");
        int pos2 = tokens.FindLastIndex(a => a.Value == "}");
        List<Token> returnableTokens = new List<Token>();
        for (int i = pos1 + 1; i < pos2; i++)
        {
            returnableTokens.Add(tokens[i]);
        }
        return returnableTokens;
    }
    public void discoverType()
    {
        if (tokens[0].Value == "if")
        {
            blockType = BlockType.If;
        }
        else if (tokens[0].Value == "while")
        {
            blockType = BlockType.While;
        }
        else if (tokens[0].Value == "for")
        {
            blockType = BlockType.For;
        }
        else
        {
            blockType = BlockType.Exec;
        }
    }

    public override string ToString()
    {
        string ret = "";
        ret += blockType + " - ";
        foreach (Token token in tokens)
        {
            ret += token.Value + " ";
        }
        ret += "\n";
        return ret;
    }


}

