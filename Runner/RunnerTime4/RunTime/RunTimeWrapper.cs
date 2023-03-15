using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

enum RunTimeActions
{
    Assigment,
    Call
}

class RunTimeWrapper
{
    public List<Token> tokens;
    private RunTimeActions runTimeType;
    

    public RunTimeWrapper(List<Token> tokens)
    {
        this.tokens = tokens;
        DecipherToken();
    }

    private object DecipherToken()
    {
        if (CheckDeclarationAndInitialize(tokens))
        {
            RunTimedataTypes dtt = RunTimedataTypes.String;
            switch (tokens[0].Value)
            {
                case "int":
                    dtt = RunTimedataTypes.Int;
                    break;
                case "float":
                    dtt = RunTimedataTypes.Float;
                    break;
                case "string":
                    dtt = RunTimedataTypes.String;
                    break;
            }
            RunTimeData.AddVariable(tokens[1].Value, tokens[3].Value, dtt);
        }
        else if (CheckDeclaration(tokens))
        {
            RunTimedataTypes dtt = RunTimedataTypes.String;
            switch (tokens[0].Value)
            {
                case "int":
                    dtt = RunTimedataTypes.Int;
                    break;
                case "float":
                    dtt = RunTimedataTypes.Float;
                    break;
                case "string":
                    dtt = RunTimedataTypes.String;
                    break;
            }
            RunTimeData.DeclareVariable(tokens[1].Value, dtt);
        }
        return true;
    }
    public bool CheckDeclaration(List<Token> tokens)
    {
        return tokens
            .Select((token, index) => new { Token = token, Index = index })
            .Where(x => x.Index <= tokens.Count - 3)
            .Any(x => x.Token.Type == TokenType.Keyword && tokens[x.Index + 1].Type == TokenType.Identifier);
    }
    public bool CheckDeclarationAndInitialize(List<Token> tokens)
    {
        return tokens
            .Select((token, index) => new { Token = token, Index = index })
            .Where(x => x.Index <= tokens.Count - 3)
            .Any(x => x.Token.Type == TokenType.Keyword && tokens[x.Index + 1].Type == TokenType.Identifier && tokens[x.Index + 2].Type == TokenType.Operator);
    }


}

